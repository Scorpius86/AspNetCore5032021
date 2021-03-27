INSERT INTO [Blog].[Post]
           ([Titulo]
           ,[Resumen]
           ,[Contenido]
		   ,[UsuarioIdPropietario]
           ,[UsuarioIdCreacion]
           ,[FechaCreacion]
           ,[UsuarioIdActualizacion]
           ,[FechaActualizacion])
     VALUES
		   ('Microservices'
           ,'"Microservices" - yet another new term on the crowded streets of software architecture. Although our natural inclination is to pass such things by with a contemptuous glance, this bit of terminology describes a style of software systems that we are finding more and more appealing. Weve seen many projects use this style in the last few years, and results so far have been positive, so much so that for many of our colleagues this is becoming the default style for building enterprise applications. Sadly, however, theres not much information that outlines what the microservice style is and how to do it.
In short, the microservice architectural style [1] is an approach to developing a single application as a suite of small services, each running in its own process and communicating with lightweight mechanisms, often an HTTP resource API. These services are built around business capabilities and independently deployable by fully automated deployment machinery. There is a bare minimum of centralized management of these services, which may be written in different programming languages and use different data storage technologies.
My Microservices Resource Guide provides links to the best articles, videos, books, and podcasts about microservices.
To start explaining the microservice style its useful to compare it to the monolithic style: a monolithic application built as a single unit. Enterprise Applications are often built in three main parts: a client-side user interface (consisting of HTML pages and javascript running in a browser on the users machine) a database (consisting of many tables inserted into a common, and usually relational, database management system), and a server-side application. The server-side application will handle HTTP requests, execute domain logic, retrieve and update data from the database, and select and populate HTML views to be sent to the browser. This server-side application is a monolith - a single logical executable[2]. Any changes to the system involve building and deploying a new version of the server-side application.'
           ,'<div class="paperBody deep">
<p>"Microservices" - yet another new term on the crowded streets
    of software architecture. Although our natural inclination is to
    pass such things by with a contemptuous glance, this bit of
    terminology describes a style of software systems that we are
    finding more and more appealing. Weve seen many projects use this
    style in the last few years, and results so far have been
    positive, so much so that for many of our colleagues this is
    becoming the default style for building enterprise
    applications. Sadly, however, theres not much information that
    outlines what the microservice style is and how to do it.</p>

<p>In short, the microservice architectural style <span class="foot-ref"><a href="#footnote-etymology">[1]</a></span> is an approach
    to developing a single application as a suite of small services,
    each running in its own process and communicating with lightweight
    mechanisms, often an HTTP resource API. These services are built
    around business capabilities and independently deployable by fully
    automated deployment machinery. There is a bare minimum of
    centralized management of these services, which may be written in
    different programming languages and use different data storage
    technologies. </p>

<aside class="sidebar" id="guide-sidebar">
<p><a href="/microservices"><img src="/microservices/microservices-sq.png"></a></p>

<p>My <a href="/microservices">Microservices Resource
      Guide</a> provides links to the best
      articles, videos, books, and podcasts about microservices.</p>
</aside>

<p>To start explaining the microservice style its useful to
    compare it to the monolithic style: a monolithic application built
    as a single unit. Enterprise Applications are often built in three main parts: a
    client-side user interface (consisting of HTML pages and
    javascript running in a browser on the users machine) a database
    (consisting of many tables inserted into a common, and usually
    relational, database management system), and a server-side
    application. The server-side application will handle HTTP
    requests, execute domain logic, retrieve and update data from the
    database, and select and populate HTML views to be sent to the
    browser. This server-side application is a <i>monolith</i> - a single
    logical executable<span class="foot-ref"><a href="#footnote-monolith">[2]</a></span>. Any changes to the
    system involve building and deploying a new version of the
    server-side application. </p>

<p>Such a monolithic server is a natural way to approach building
    such a system. All your logic for handling a request runs in a
    single process, allowing you to use the basic features of your
    language to divide up the application into classes, functions, and
    namespaces. With some care, you can run and test the application
    on a developers laptop, and use a deployment pipeline to ensure
    that changes are properly tested and deployed into production. You
    can horizontally scale the monolith by running many instances
    behind a load-balancer.</p>

<p>Monolithic applications can be successful, but increasingly
    people are feeling frustrations with them - especially as more
    applications are being deployed to the cloud . Change cycles are
    tied together - a change made to a small part of the application,
    requires the entire monolith to be rebuilt and deployed. Over time
    its often hard to keep a good modular structure, making it harder
    to keep changes that ought to only affect one module within that
    module. Scaling requires scaling of the entire application rather
    than parts of it that require greater resource. </p>

<div class="figure " id="microservices_images_sketch.png"><img src="microservices/images/sketch.png">
<p class="photoCaption">Figure 1: Monoliths
    and Microservices</p>
</div>

<div class="clear"></div>

<p>These frustrations have led to the microservice architectural
    style: building applications as suites of services. As well as the
    fact that services are independently deployable and scalable, each
    service also provides a firm module boundary, even allowing for
    different services to be written in different programming
    languages. They can also be managed by different teams .</p>

<p>We do not claim that the microservice style is novel
    or innovative, its roots go back at least to the design principles
    of Unix. But we do think that not enough people consider a
    microservice architecture and that many software developments
    would be better off if they used it.</p>

<section id="CharacteristicsOfAMicroserviceArchitecture">
<h2>Characteristics of a Microservice Architecture</h2>

<p>We cannot say there is a formal definition of the
      microservices architectural style, but we can attempt to
      describe what we see as common characteristics for architectures
      that fit the label. As with any definition that outlines common
      characteristics, not all microservice architectures have all the
      characteristics, but we do expect that most microservice
      architectures exhibit most characteristics. While we authors
      have been active members of this rather loose community, our
      intention is to attempt a description of what we see in our own
      work and in similar efforts by teams we know of. In particular
      we are not laying down some definition to conform to. </p>

<section id="ComponentizationViaServices">
<h3>Componentization via Services</h3>

<p>For as long as weve been involved in the software
        industry, theres been a desire to build systems by plugging
        together components, much in the way we see things are made in
        the physical world. During the last couple of decades weve
        seen considerable progress with large compendiums of common
        libraries that are part of most language platforms.</p>

<p>When talking about components we run into the difficult
        definition of what makes a component. <a href="/bliki/SoftwareComponent.html">Our definition</a> is that a
        <b>component</b> is a unit of software that is
        independently replaceable and upgradeable.</p>

<p>Microservice architectures will use libraries, but their
        primary way of componentizing their own software is by
        breaking down into services. We define <b>libraries</b>
        as components that are linked into a program and called using
        in-memory function calls, while <b>services</b> are
        out-of-process components who communicate with a mechanism such
        as a web service request, or remote procedure call. (This is a
        different concept to that of a service object in many OO
        programs <span class="foot-ref"><a href="#footnote-service-object">[3]</a></span>.)</p>

<p>One main reason for using services as components (rather
        than libraries) is that services are independently deployable.
        If you have an application <span class="foot-ref"><a href="#footnote-application">[4]</a></span> that consists of a multiple
        libraries in a single process, a change to any single component
        results in having to redeploy the entire application. But if
        that application is decomposed into multiple services, you can
        expect many single service changes to only require
        that service to be redeployed. Thats not an absolute, some
        changes will change service interfaces resulting in some
        coordination, but the aim of a good microservice architecture is
        to minimize these through cohesive service boundaries and
        evolution mechanisms in the service contracts.</p>

<p>Another consequence of using services as components is a
        more explicit component interface. Most languages do not have
        a good mechanism for defining an explicit <a href="/bliki/PublishedInterface.html">Published Interface</a>. Often its only documentation and
        discipline that prevents clients breaking a components
        encapsulation, leading to overly-tight coupling between
        components. Services make it easier to avoid this by using
        explicit remote call mechanisms.</p>

<p>Using services like this does have downsides. Remote calls
        are more expensive than in-process calls, and thus remote APIs
        need to be coarser-grained, which is often more awkward to
        use. If you need to change the allocation of responsibilities
        between components, such movements of behavior are harder to
        do when youre crossing process boundaries.</p>

<p>At a first approximation, we can observe that services map
        to runtime processes, but that is only a first approximation.
        A service may consist of multiple processes that will always
        be developed and deployed together, such as an application
        process and a database thats only used by that service. </p>
</section>

<section id="OrganizedAroundBusinessCapabilities">
<h3>Organized around Business Capabilities</h3>

<p>When looking to split a large application into parts,
        often management focuses on the technology layer, leading to
        UI teams, server-side logic teams, and database teams. When
        teams are separated along these lines, even simple changes can
        lead to a cross-team project taking time and budgetary approval. A smart team will
        optimise around this and plump for the lesser of two evils -
        just force the logic into whichever application they have
        access to. Logic everywhere in other words. This is an example
        of Conways Law<span class="foot-ref"><a href="#footnote-conwayslaw">[5]</a></span> in action.</p>

<blockquote>
<p>Any organization that designs a system (defined broadly)
          will produce a design whose structure is a copy of the
          organizations communication structure.</p>

<p class="quote-attribution">-- Melvin Conway, 1968</p>
</blockquote>

<div class="figure " id="microservices_images_conways-law.png"><img src="microservices/images/conways-law.png">
<p class="photoCaption">Figure 2: Conways
        Law in action</p>
</div>

<div class="clear"></div>

<p>The microservice approach to division is different,
        splitting up into services organized around
        <b>business capability</b>. Such services take a
        broad-stack implementation of software for that business area,
        including user-interface, persistant storage, and any external
        collaborations. Consequently the teams are cross-functional,
        including the full range of skills required for the
        development: user-experience, database, and project
        management. </p>

<div class="figure " id="microservices_images_PreferFunctionalStaffOrganization.png"><img src="microservices/images/PreferFunctionalStaffOrganization.png">
<p class="photoCaption">Figure 3: Service
        boundaries reinforced by team boundaries</p>
</div>

<div class="clear"></div>

<aside class="sidebar" id="HowBigIsAMicroservice">
<h3>How big is a microservice?</h3>

<p>Although “microservice” has become a popular name for this
          architectural style, its name does lead to an unfortunate
          focus on the size of service, and arguments about what
          constitutes “micro”. In our conversations with microservice
          practitioners, we see a range of sizes of services. The
          largest sizes reported follow Amazons notion of the Two
          Pizza Team (i.e. the whole team can be fed by two pizzas),
          meaning no more than a dozen people. On the smaller size
          scale weve seen setups where a team of half-a-dozen would
          support half-a-dozen services.</p>

<p>This leads to the question of whether there are
          sufficiently large differences within this size range that
          the service-per-dozen-people and service-per-person sizes
          shouldnt be lumped under one microservices label. At the
          moment we think its better to group them together, but
          its certainly possible that well change our mind as we
          explore this style further.</p>
</aside>

<p>One company organised in this way is <a href="http://www.comparethemarket.com">www.comparethemarket.com</a>.
        Cross functional teams are responsible for building and operating
        each product and each product is split out into a number of
        individual services communicating via a message bus.</p>

<p>Large monolithic applications can always be modularized
        around business capabilities too, although thats not the
        common case. Certainly we would urge a large team building a
        monolithic application to divide itself along business lines.
        The main issue we have seen here, is that they tend to be
        organised around <i>too many</i> contexts. If the monolith
        spans many of these modular boundaries it can be difficult for individual
        members of a team to fit them into their short-term
        memory. Additionally we see that the modular
        lines require a great deal of discipline to enforce. The
        necessarily more explicit separation required by service
        components makes it easier to keep the team boundaries clear.</p>
</section>

<section id="ProductsNotProjects">
<h3>Products not Projects</h3>

<p>Most application development efforts that we see use a
        project model: where the aim is to deliver some piece of
        software which is then considered to be completed. On
        completion the software is handed over to a
        maintenance organization and the project team that built it is
        disbanded.</p>

<p>Microservice proponents tend to avoid this model,
        preferring instead the notion that a team should own a product
        over its full lifetime. A common inspiration for this is
        Amazons notion of <a href="https://queue.acm.org/detail.cfm?id=1142065">"you build, you
        run it"</a> where a development team takes full responsibility
        for the software in production. This brings developers into
        day-to-day contact with how their software behaves in
        production and increases contact with their users, as they
        have to take on at least some of the support burden.</p>

<p>The product mentality, ties in with the linkage to business
        capabilities. Rather than looking at the software as a set of
        functionality to be completed, there is an on-going
        relationship where the question is how can software assist its
        users to enhance the business capability.</p>

<p>Theres no reason why this same approach cant be taken
        with monolithic applications, but the smaller granularity of
        services can make it easier to create the personal
        relationships between service developers and their users.</p>
</section>

<section id="SmartEndpointsAndDumbPipes">
<h3>Smart endpoints and dumb pipes</h3>

<p>When building communication structures between different
          processes, weve seen many products and approaches that stress
          putting significant smarts into the communication mechanism
          itself. A good example of this is the Enterprise Service Bus
          (ESB), where ESB products often include sophisticated
          facilities for message routing, choreography, transformation,
          and applying business rules.</p>

<aside class="sidebar" id="MicroservicesAndSoa">
<h3>Microservices and SOA</h3>

<p>When weve talked about microservices a common question is
            whether this is just Service Oriented Architecture (SOA) that we
            saw a decade ago. There is merit to this point, because the
            microservice style is very similar to what some advocates of SOA
            have been in favor of. The problem, however, is that SOA means <a href="/bliki/ServiceOrientedAmbiguity.html">too
            many different things</a>, and that most of the time that we come
            across something called "SOA" its significantly different to the
            style were describing here, usually due to a focus on ESBs used
            to integrate monolithic applications.</p>

<p>In particular we have seen so many botched implementations of
            service orientation - from the tendency to hide complexity away
            in ESBs <span class="foot-ref"><a href="#footnote-esb">[6]</a></span>, to failed multi-year initiatives
            that cost millions and deliver no value, to centralised
            governance models that actively inhibit change, that it is
            sometimes difficult to see past these problems.</p>

<p>Certainly, many of the techniques in use in the microservice
            community have grown from the experiences of developers
            integrating services in large organisations. The <a href="/bliki/TolerantReader.html">Tolerant Reader</a> pattern is an example of this. Efforts
            to use the web have contributed, using simple protocols is
            another approach derived from these experiences - a reaction
            away from central standards that have reached a complexity that
            is, <a href="http://wiki.apache.org/ws/WebServiceSpecifications">frankly,
            breathtaking</a>. (Any time you need an ontology to manage your
            ontologies you know you are in deep trouble.)</p>

<p>This common manifestation of SOA has led some microservice
            advocates to reject the SOA label entirely, although others
            consider microservices to be one form of SOA <span class="foot-ref"><a href="#footnote-fine-grained">[7]</a></span>, perhaps <i>service orientation done
            right</i>. Either way, the fact that SOA means such different
            things means its valuable to have a term that more crisply
            defines this architectural style.</p>
</aside>

<p>The microservice community favours an alternative approach:
          <i>smart endpoints and dumb pipes</i>. Applications
          built from microservices aim to be as decoupled and as
          cohesive as possible - they own their own domain logic and act
          more as filters in the classical Unix sense - receiving a
          request, applying logic as appropriate and producing a
          response. These are choreographed using simple RESTish protocols rather
          than complex protocols such as WS-Choreography or BPEL or
          orchestration by a central tool.</p>

<p>The two protocols used most commonly are HTTP
          request-response with resource APIs and lightweight
          messaging<span class="foot-ref"><a href="#footnote-protobufs">[8]</a></span>. The best expression of
          the first is</p>

<blockquote>
<p>Be of the web, not behind the web </p>

<p class="quote-attribution">-- <a href="https://www.amazon.com/gp/product/0596805829?ie=UTF8&amp;tag=martinfowlerc-20&amp;linkCode=as2&amp;camp=1789&amp;creative=9325&amp;creativeASIN=0596805829">Ian Robinson</a><img src="https://www.assoc-amazon.com/e/ir?t=martinfowlerc-20&amp;l=as2&amp;o=1&amp;a=0321601912" width="1" height="1" border="0" alt="" style="width: 1px !important; height: 1px !important; border:none !important; margin:0px !important;" papexlftl=""></p>
</blockquote>

<p>Microservice teams use the principles and
          protocols that the world wide web (and to a large extent,
          Unix) is built on. Often used resources can be cached with very
          little effort on the part of developers or operations
          folk. </p>

<p>The second approach in common use is messaging over a
          lightweight message bus. The infrastructure chosen is
          typically dumb (dumb as in acts as a message router only) -
          simple implementations such as RabbitMQ or ZeroMQ dont do
          much more than provide a reliable asynchronous fabric - the
          smarts still live in the end points that are producing and
          consuming messages; in the services.</p>

<p>In a monolith, the components are executing in-process and
          communication between them is via either method invocation or
          function call. The biggest issue in changing a monolith into
          microservices lies in changing the communication pattern. A
          naive conversion from in-memory method calls to RPC leads to
          chatty communications which dont perform well. Instead you
          need to replace the fine-grained communication with a coarser
          -grained approach.</p>
</section>

<section id="DecentralizedGovernance">
<h3>Decentralized Governance</h3>

<p>One of the consequences of centralised governance is the
        tendency to standardise on single technology
        platforms. Experience shows that this approach is constricting
        - not every problem is a nail and not every solution a
        hammer. We prefer using the right tool for the job and
        while monolithic applications can take advantage of different
        languages to a certain extent, it isnt that common.</p>

<p>Splitting the monoliths components out into services we
        have a choice when building each of them. You want to use
        Node.js to standup a simple reports page? Go for it. C++ for a
        particularly gnarly near-real-time component? Fine. You want
        to swap in a different flavour of database that better suits
        the read behaviour of one component? We have the technology to
        rebuild him.</p>

<p>Of course, just because you <i>can</i> do something,
        doesnt mean you <i>should</i> - but partitioning your system
        in this way means you have the option.</p>

<p>Teams building microservices prefer a different approach to
        standards too. Rather than use a set of defined standards
        written down somewhere on paper they prefer the idea of
        producing useful tools that other developers can use to solve
        similar problems to the ones they are facing. These tools are
        usually harvested from implementations and shared with a wider
        group, sometimes, but not exclusively using an internal open
        source model. Now that git and github have become the de facto
        version control system of choice, open source practices are
        becoming more and more common in-house .</p>

<p>Netflix is a good example of an organisation that follows
        this philosophy. Sharing useful and, above all, battle-tested
        code as libraries encourages other developers to solve similar
        problems in similar ways yet leaves the door open to picking a
        different approach if required. Shared libraries tend to be
        focused on common problems of data storage, inter-process
        communication and as we discuss further below, infrastructure
        automation.</p>

<p>For the microservice community, overheads are particularly
        unattractive. That isnt to say that the community doesnt
        value service contracts. Quite the opposite, since there tend
        to be many more of them. Its just that they are looking at
        different ways of managing those contracts. Patterns like
        <a href="/bliki/TolerantReader.html">Tolerant Reader</a> and <a href="/articles/consumerDrivenContracts.html">Consumer-Driven
        Contracts</a> are often applied to microservices. These aid
        service contracts in evolving independently. Executing
        consumer driven contracts as part of your build increases
        confidence and provides fast feedback on whether your services
        are functioning. Indeed we know of a team in Australia who
        drive the build of new services with consumer driven
        contracts. They use simple tools that allow them to define the
        contract for a service. This becomes part of the automated
        build before code for the new service is even written. The
        service is then built out only to the point where it satisfies
        the contract - an elegant approach to avoid the
        YAGNI<span class="foot-ref"><a href="#footnote-YAGNI">[9]</a></span> dilemma when building new
        software. These techniques and the tooling growing up around
        them, limit the need for central contract management by
        decreasing the temporal coupling between services. </p>

<aside class="sidebar" id="ManyLanguagesManyOptions">
<h3>Many languages, many options</h3>

<p>The growth of JVM as a platform is just the latest
          example of
          mixing languages within a common platform. Its been common
          practice to shell-out to a higher level language to take advantage
          of higher level abstractions for decades. As is dropping down to
          the metal and writing performance sensitive code in a lower level
          one. However, many monoliths dont need this level of performance
          optimisation nor are DSLs and higher level abstractions that
          common (to our dismay). Instead monoliths are usually single
          language and the tendency is to limit the number of technologies
          in use <span class="foot-ref"><a href="#footnote-many-languages">[10]</a></span>.</p>
</aside>

<p>Perhaps the apogee of decentralised governance is the build
        it / run it ethos popularised by Amazon. Teams are responsible
        for all aspects of the software they build including operating
        the software 24/7. Devolution of this level of responsibility
        is definitely not the norm but we do see more and more
        companies pushing responsibility to the development
        teams. Netflix is another organisation that has adopted this
        ethos<span class="foot-ref"><a href="#footnote-netflix-flowcon">[11]</a></span>. Being woken up at 3am
        every night by your pager is certainly a powerful incentive to
        focus on quality when writing your code. These ideas are about
        as far away from the traditional centralized governance model
        as it is possible to be.</p>
</section>

<section id="DecentralizedDataManagement">
<h3>Decentralized Data Management</h3>

<p>Decentralization of data management presents in a number of
        different ways. At the most abstract level, it means that the
        conceptual model of the world will differ between systems.
        This is a common issue when integrating across a large
        enterprise, the sales view of a customer will differ from the
        support view. Some things that are called customers in the
        sales view may not appear at all in the support view. Those
        that do may have different attributes and (worse) common
        attributes with subtly different semantics.</p>

<aside class="sidebar" id="Battle-testedStandardsAndEnforcedStandards">
<h3>Battle-tested standards and enforced standards</h3>

<p>Its a bit of a dichotomy that microservice teams tend to
          eschew the kind of rigid enforced standards laid down by
          enterprise architecture groups but will happily use and even
          evangelise the use of open standards such as HTTP, ATOM and
          other microformats.</p>

<p>The key difference is how the standards are developed and
          how they are enforced. Standards managed by groups such as
          the IETF only <i>become</i> standards when there are several
          live implementations of them in the wider world and which
          often grow from successful open-source projects.</p>

<p>These standards are a world apart from many in a
          corporate world, which are often developed by groups that
          have little recent programming experience or overly influenced
          by vendors.</p>
</aside>

<p>This issue is common between applications, but can also
        occur <i>within</i> applications, particular when that
        application is divided into separate components. A useful way
        of thinking about this is the Domain-Driven Design notion of
        <a href="/bliki/BoundedContext.html">Bounded Context</a>. DDD divides a complex
        domain up into multiple bounded contexts and maps out the
        relationships between them. This process is useful
        for both monolithic and microservice architectures, but there
        is a natural correlation between service and context
        boundaries that helps clarify, and as we describe in the
        section on business capabilities, reinforce the
        separations.</p>

<p>As well as decentralizing decisions about conceptual
        models, microservices also decentralize data storage
        decisions. While monolithic applications prefer a single logical
        database for persistant data, enterprises often prefer a
        single database across a range of applications - many of these
        decisions driven through vendors commercial models around
        licensing.  Microservices prefer letting each service manage
        its own database, either different instances of the same
        database technology, or entirely different database systems -
        an approach called <a href="/bliki/PolyglotPersistence.html">Polyglot Persistence</a>. You
        can use polyglot persistence in a monolith, but it appears
        more frequently with microservices.</p>

<div class="figure " id="microservices_images_decentralised-data.png"><img src="microservices/images/decentralised-data.png">
<p class="photoCaption"></p>
</div>

<div class="clear"></div>

<p>Decentralizing responsibility for data across microservices
        has implications for managing updates. The common
        approach to dealing with updates has been to use transactions
        to guarantee consistency when updating multiple resources.
        This approach is often used within monoliths.</p>

<p>Using transactions like this helps with consistency, but
        imposes significant temporal coupling, which is problematic
        across multiple services. Distributed transactions are
        notoriously difficult to implement and as a consequence
        microservice architectures <a href="http://www.eaipatterns.com/ramblings/18_starbucks.html">emphasize
        transactionless coordination between services</a>, with
        explicit recognition that consistency may only be eventual
        consistency and problems are dealt with by compensating
        operations.</p>

<p>Choosing to manage inconsistencies in this way is a new
  challenge for many development teams, but it is one that often
  matches business practice. Often businesses handle a degree of
  inconsistency in order to respond quickly to demand, while
  having some kind of reversal process to deal with
  mistakes. The trade-off is worth it as long as the cost of
  fixing mistakes is less than the cost of lost business under
  greater consistency.</p>
</section>

<section id="InfrastructureAutomation">
<h3>Infrastructure Automation</h3>

<p>Infrastructure automation techniques have evolved
        enormously over the last few years - the evolution of the
        cloud and AWS in particular has reduced the operational
        complexity of building, deploying and operating
        microservices.</p>

<p>Many of the products or systems being build with
        microservices are being built by teams with extensive
        experience of <a href="/bliki/ContinuousDelivery.html">Continuous Delivery</a> and its
        precursor, <a href="/articles/continuousIntegration.html">Continuous
        Integration</a>. Teams building software this way make
  extensive use of infrastructure automation techniques. This is
  illustrated in the build pipeline shown below.</p>

<div class="figure " id="microservices_images_basic-pipeline.png"><img src="microservices/images/basic-pipeline.png">
<p class="photoCaption">Figure 5: basic
        build pipeline</p>
</div>

<div class="clear"></div>

<p>Since this isnt an article on Continuous Delivery we will
        call attention to just a couple of key features here. We want
        as much confidence as possible that our software is working,
        so we run lots of <b>automated tests</b>. Promotion of working
        software up the pipeline means we <b>automate deployment</b>
        to each new environment.</p>

<aside class="sidebar" id="MakeItEasyToDoTheRightThing">
<h3>Make it easy to do the right thing</h3>

<p>One side effect we have found of increased automation as
          a consequence of continuous delivery and deployment is the
          creation of useful tools to help developers and operations
          folk. Tooling for creating artefacts, managing codebases,
          standing up simple services or for adding standard
          monitoring and logging are pretty common now. The best
          example on the web is probably <a href="http://netflix.github.io/">Netflixs set of open
          source tools</a>, but there are others including <a href="http://dropwizard.codahale.com/">Dropwizard</a> which
          we have used extensively.</p>
</aside>

<p>A monolithic application will be built, tested and pushed
        through these environments quite happlily. It turns out that
        once you have invested in automating the path to production
        for a monolith, then deploying <i>more</i> applications
        doesnt seem so scary any more. Remember, one of the aims of
        CD is to make deployment boring, so whether its one or three
        applications, as long as its still boring it doesnt
        matter<span class="foot-ref"><a href="#footnote-trickycd">[12]</a></span>. </p>

<p>Another area where we see teams using extensive
        infrastructure automation is when managing microservices in
        production. In contrast to our assertion above that as long as
        deployment is boring there isnt that much difference between
        monoliths and microservices, the operational landscape for
        each can be strikingly different.</p>

<div class="figure " id="microservices_images_micro-deployment.png"><img src="microservices/images/micro-deployment.png">
<p class="photoCaption">Figure 6: Module
        deployment often differs</p>
</div>

<div class="clear"></div>
</section>

<section id="DesignForFailure">
<h3>Design for failure</h3>

<p>A consequence of using services as components, is that
        applications need to be designed so that they can tolerate the
        failure of services. Any service call could fail due to
        unavailability of the supplier, the client has to respond to
        this as gracefully as possible. This is a disadvantage
        compared to a monolithic design as it introduces additional
        complexity to handle it. The consequence is that microservice
        teams constantly reflect on how service failures affect the
        user experience. Netflixs <a href="https://github.com/Netflix/SimianArmy">Simian Army</a>
        induces failures of services and even datacenters during the
        working day to test both the applications resilience and
        monitoring.</p>

<aside class="sidebar" id="TheCircuitBreakerAndProductionReadyCode">
<h3>The circuit breaker and production ready code</h3>

<p><a href="/bliki/CircuitBreaker.html">Circuit Breaker</a> appears in <a href="https://www.amazon.com/gp/product/B00A32NXZO?ie=UTF8&amp;tag=martinfowlerc-20&amp;linkCode=as2&amp;camp=1789&amp;creative=9325&amp;creativeASIN=B00A32NXZO">Release It!</a><img src="https://www.assoc-amazon.com/e/ir?t=martinfowlerc-20&amp;l=as2&amp;o=1&amp;a=0321601912" width="1" height="1" border="0" alt="" style="width: 1px !important; height: 1px !important; border:none !important; margin:0px !important;" papexlftl=""> alongside other
    patterns such as Bulkhead and Timeout. Implemented together,
    these patterns are crucially important when building
    communicating applications. This <a href="http://techblog.netflix.com/2012/02/fault-tolerance-in-high-volume.html">Netflix
    blog entry</a> does a great job of explaining their
    application of them.</p>
</aside>

<p>This kind of automated testing in production would be
        enough to give most operation groups the kind of shivers
        usually preceding a week off work. This isnt to say that
        monolithic architectural styles arent capable of
        sophisticated monitoring setups - its just less common in our
        experience.</p>

<p>Since services can fail at any time, its important to be
        able to detect the failures quickly and, if possible,
        automatically restore service. Microservice applications put a
        lot of emphasis on real-time monitoring of the application,
        checking both architectural elements (how many requests per
        second is the database getting) and business relevant metrics
        (such as how many orders per minute are received). Semantic
        monitoring can provide an early warning system of something
        going wrong that triggers development teams to follow up and
        investigate.</p>

<p>This is particularly important to a microservices
        architecture because the microservice preference towards
        choreography and <a href="/eaaDev/EventCollaboration.html">event collaboration</a>
        leads to emergent behavior. While many pundits praise the
        value of serendipitous emergence, the truth is that emergent
        behavior can sometimes be a bad thing. Monitoring is vital to
        spot bad emergent behavior quickly so it can be fixed.</p>

<aside class="sidebar" id="SynchronousCallsConsideredHarmful">
<h3>Synchronous calls considered harmful</h3>

<p>Any time you have a number of synchronous calls between services you will
    encounter the multiplicative effect of downtime. Simply,
    this is when the downtime of your system becomes the product
    of the downtimes of the individual components. You face a
    choice, making your calls asynchronous or managing
    the downtime. At www.guardian.co.uk they have implemented a
    simple rule on the new platform - one synchronous call per
    user request while at Netflix, their platform API redesign
    has built asynchronicity into the API fabric.</p>
</aside>

<p>Monoliths can be built to be as transparent as a
        microservice - in fact, they should be. The difference is that
        you absolutely need to know when services running in different
        processes are disconnected. With libraries within the same
        process this kind of transparency is less likely to be
        useful.</p>

<p>Microservice teams would expect to see sophisticated
        monitoring and logging setups for each individual
        service such as dashboards showing up/down status and a variety of
        operational and business relevant metrics. Details on circuit
        breaker status, current throughput and latency are other
        examples we often encounter in the wild.</p>
</section>

<section id="EvolutionaryDesign">
<h3>Evolutionary Design</h3>

<p>Microservice practitioners, usually have come from
        an evolutionary design background and see service
        decomposition as a further tool to enable application
        developers to control changes in their application without
        slowing down change. Change control doesnt necessarily mean
        change reduction - with the right attitudes and tools you can
        make frequent, fast, and well-controlled changes to
        software.</p>

<p>Whenever you try to break a software system into
        components, youre faced with the decision of how to divide up
        the pieces - what are the principles on which we decide to
        slice up our application? The key property of a component is
        the notion of independent replacement and
        upgradeability<span class="foot-ref"><a href="#footnote-RCA">[13]</a></span> - which implies we look for
        points where we can imagine rewriting a component without
        affecting its collaborators.  Indeed many microservice groups
        take this further by explicitly expecting many services to be
        scrapped rather than evolved in the longer term.</p>

<p>The Guardian website is a good example of an application
        that was designed and built as a monolith, but has been
        evolving in a microservice direction. The monolith still is
        the core of the website, but they prefer to add new features
        by building microservices that use the monoliths API. This
        approach is particularly handy for features that are
        inherently temporary, such as specialized pages to handle a
        sporting event. Such a part of the website can quickly be put
        together using rapid development languages, and removed once
        the event is over. Weve seen similar approaches at a
        financial institution where new services are added for a
        market opportunity and discarded after a few months or even
        weeks.</p>

<p>This emphasis on replaceability is a special case of a more
        general principle of modular design, which is to drive
        modularity through the pattern of change <span class="foot-ref"><a href="#footnote-beck-rate-of-change">[14]</a></span>. You want to keep things that change
        at the same time in the same module. Parts of a system that
        change rarely should be in different services to those that
        are currently undergoing lots of churn. If you find yourself
        repeatedly changing two services together, thats a sign that
        they should be merged.</p>

<p>Putting components into services adds an opportunity for
        more granular release planning. With a monolith any changes
        require a full build and deployment of the entire
        application. With microservices, however, you only need to
        redeploy the service(s) you modified. This can simplify and
        speed up the release process. The downside is that you have to
        worry about changes to one service breaking its
        consumers. The traditional integration approach is to try to deal
        with this problem using versioning, but the preference in the
        microservice world is to <a href="/articles/enterpriseREST.html#versioning">only
        use versioning as a last resort</a>. We can avoid a lot of
        versioning by designing services to be as tolerant as possible
        to changes in their suppliers.</p>
</section>
</section>

<section id="AreMicroservicesTheFuture">
<h2>Are Microservices the Future?</h2>

<p>Our main aim in writing this article is to explain the major
      ideas and principles of microservices. By taking the time to do
      this we clearly think that the microservices architectural style
      is an important idea - one worth serious consideration for
      enterprise applications. We have recently built several systems
      using the style and know of others who have used and favor this
      approach.</p>

<div class="article-card">
<h3><a href="/articles/microservice-trade-offs.html">Microservice Trade-Offs</a></h3>

<div class="card-body">
<p class="card-image"><img src="https://martinfowler.com/articles/microservice-trade-offs/card.png"></p>

<p class="abstract">
    Many development teams have found the microservices architectural style to be
    a superior approach to a monolithic architecture. But other teams
    have found them to be a productivity-sapping burden. Like any
    architectural style, microservices bring costs and benefits. To
    make a sensible choice you have to understand these and apply them
    to your specific context.
  </p>

<div class="meta">
<p class="credits">by Martin Fowler</p>

<p class="date">1 Jul 2015</p>

<p class="more"><a href="/articles/microservice-trade-offs.html">Read more…</a></p>

<p class="type article">article</p>

<p class="tags"> <span class="tag-link"><a href="/tags/microservices.html">microservices</a></span> </p>
</div>
</div>
</div>

<p>Those we know about who are in some way pioneering the
      architectural style include Amazon, Netflix, <a href="http://www.theguardian.com">The Guardian</a>, the <a href="https://gds.blog.gov.uk/">UK Government Digital Service</a>, <a href="realestate.com.au">realestate.com.au</a>, Forward and <a href="http://www.comparethemarket.com/">comparethemarket.com</a>. The
      conference circuit in 2013 was full of examples of companies
      that are moving to something that would class as microservices -
      including Travis CI. In addition there are plenty of
      organizations that have long been doing what we would class as
      microservices, but without ever using the name. (Often this is
      labelled as SOA - although, as weve said, SOA comes in many
      contradictory forms. <span class="foot-ref"><a href="#footnote-already">[15]</a></span>) </p>

<p>Despite these positive experiences, however, we arent
      arguing that we are certain that microservices are the future
      direction for software architectures. While our experiences so
      far are positive compared to monolithic applications, were
      conscious of the fact that not enough time has passed for us to
      make a full judgement.</p>

<p>Often the true consequences of your architectural decisions
      are only evident several years after you made them. We have seen
      projects where a good team, with a strong desire for
      modularity, has built a monolithic architecture that has
      decayed over the years. Many people believe that such decay is
      less likely with microservices, since the service boundaries are
      explicit and hard to patch around. Yet until we see enough
      systems with enough age, we cant truly assess how microservice
      architectures mature.</p>

<p>There are certainly reasons why one might expect
      microservices to mature poorly. In any effort at
      componentization, success depends on how well the software fits
      into components. Its hard to figure out exactly where the
      component boundaries should lie. Evolutionary design recognizes
      the difficulties of getting boundaries right and thus the
      importance of it being easy to refactor them. But when your
      components are services with remote communications, then
      refactoring is much harder than with in-process libraries.
      Moving code is difficult across service boundaries, any
      interface changes need to be coordinated between participants,
      layers of backwards compatibility need to be added, and testing
      is made more complicated.</p>

<div class="book-sidebar"><span class="img-link"><a href="https://www.amazon.com/gp/product/1491950358?ie=UTF8&amp;tag=martinfowlerc-20&amp;linkCode=as2&amp;camp=1789&amp;creative=9325&amp;creativeASIN=1491950358"><img class="cover" src="microservices/images/sam-book.jpg"></a><img src="https://www.assoc-amazon.com/e/ir?t=martinfowlerc-20&amp;l=as2&amp;o=1&amp;a=0321601912" width="1" height="1" border="0" alt="" style="width: 1px !important; height: 1px !important; border:none !important; margin:0px !important;" papexlftl=""></span>
<p>Our colleague Sam Newman spent most of 2014 working on a
        book that captures our experiences with building
        microservices. This should be your next step if you want a deeper
        dive into the topic.</p>
</div>

<p>Another issue is If the components do not compose cleanly, then
      all you are doing is shifting complexity from inside a component
      to the connections between components. Not just does this just
      move complexity around, it moves it to a place thats less
      explicit and harder to control. Its easy to think things are
      better when you are looking at the inside of a small, simple
      component, while missing messy connections between services.</p>

<p>Finally, there is the factor of team skill. New techniques
      tend to be adopted by more skillful teams. But a technique that
      is more effective for a more skillful team isnt necessarily
      going to work for less skillful teams. Weve seen plenty of
      cases of less skillful teams building messy monolithic
      architectures, but it takes time to see what happens when this
      kind of mess occurs with microservices. A poor team will always
      create a poor system - its very hard to tell if microservices
      reduce the mess in this case or make it worse.</p>

<p>One reasonable argument weve heard is that you shouldnt
      start with a microservices architecture. Instead
      <a href="/bliki/MonolithFirst.html">begin with a monolith</a>,
      keep it modular, and split it into microservices once the
      monolith becomes a problem. (Although
      <a href="/articles/dont-start-monolith.html">this advice isnt ideal</a>,
      since a good in-process interface is usually not a good service interface.)</p>

<p>So we write this with cautious optimism. So far, weve seen
      enough about the microservice style to feel that it can be
      <a href="/microservices/">a worthwhile road to tread</a>.
      We cant say for sure where well end
      up, but one of the challenges of software development is that
      you can only make decisions based on the imperfect information
      that you currently have to hand.</p>
</section>

<hr class="bodySep">
</div>'
    ,2
    ,2
    ,GETDATE()
    ,2
    ,GETDATE());