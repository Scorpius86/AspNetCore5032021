using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Net5.AspNet.Exam.Client.MVC.Models;
using Net5.AspNet.Exam.Infrastructure.Data.Security.Entities;
using Net5.AspNet.Exam.Infrastructure.Security.Constans;

namespace Net5.AspNet.Exam.Client.MVC.Areas.Identity.Pages.Account.Manage
{   
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Sur Name")]
            public string SurName { get; set; }

            [Required(ErrorMessage = "The Roles field is required.")]
            [Display(Name = "Roles")]
            public string SelectedRoles { get; set; }
            public List<RoleViewModel> Roles { get; set; }            
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            var roles = _mapper.Map<List<RoleViewModel>>(_roleManager.Roles.ToList());
            var userRoles = await _userManager.GetRolesAsync(user);
            var userRolesId = _roleManager.Roles.Where(r => userRoles.Contains(r.Name)).Select(r => r.Id).ToList();


            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                SurName = user.SurName,
                Roles = roles,
                SelectedRoles = string.Join(",", userRolesId)
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.SurName = Input.SurName;
            await _userManager.UpdateAsync(user);

            
            
            var userRoles = await _userManager.GetRolesAsync(user);            
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            var rolesId = Input.SelectedRoles.Split(',').ToList();
            var roles = _roleManager.Roles.Where(r => rolesId.Contains(r.Id)).Select(r => r.Name).ToList();
            await _userManager.AddToRolesAsync(user, roles);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
