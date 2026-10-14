using Portfolio.Models;

namespace Portfolio.Services;

public static class ProfileData
{
    public static readonly string[] Languages = ["en", "ru", "kk"];

    public static string Normalize(string? lang) => lang?.ToLowerInvariant() switch
    {
        "ru" => "ru",
        "kk" => "kk",
        _ => "en"
    };

    public static Profile Get(string? lang) => Normalize(lang) switch
    {
        "ru" => Russian,
        "kk" => Kazakh,
        _ => English
    };

    private static readonly Contacts Contacts = new(
        Telegram: "@sozakbay",
        Email: "eldos.sozakbay@gmail.com",
        Phone: "+7 (777) 190-54-80",
        GitHubUrl: "https://github.com/yeldos13",
        LinkedInUrl: "https://www.linkedin.com/in/yeldos-sozakbay-4b38a8209/");

    public const string CvPath = "/files/Yeldos-Sozakbay-CV.pdf";

    public const string DisplayName = "Yeldos Sozakbay";

    public const string SiteUrl = "https://sozakbay.dev";

    private static IReadOnlyList<SkillGroup> SkillGroups(string languages, string platform, string data, string oop) =>
    [
        new(languages, "purple",
        [
            new("C#", "C#", "#9b4f96"),
            new("SQL", "SQL", "#336791"),
            new("JavaScript", "JS", "#c9a227"),
            new("Java", "JV", "#e76f00"),
            new("HTML", "HT", "#e34c26"),
            new("UML", "UM", "#5a6b8c"),
        ]),
        new(platform, "blue",
        [
            new(".NET", "NT", "#512bd4"),
            new("MVC", "MV", "#3b82f6"),
            new("Windows Forms", "WF", "#0078d4"),
            new("REST", "RS", "#10b981"),
            new("SOAP", "SP", "#0ea5e9"),
            new(oop, "OO", "#8b5cf6"),
            new("Unit Testing", "UT", "#22c55e"),
        ]),
        new(data, "red",
        [
            new("PostgreSQL", "PG", "#336791"),
            new("MS SQL Server", "MS", "#cc2927"),
            new("NoSQL", "NS", "#4db33d"),
            new("Apache Kafka", "KF", "#6b7280"),
            new("Docker", "DK", "#2496ed"),
            new("Git", "GT", "#f05032"),
            new("CI/CD", "CI", "#f59e0b"),
            new("Swagger", "SW", "#85ea2d"),
            new("Jira", "JR", "#0052cc"),
            new("StimulSoft", "SS", "#e11d48"),
            new("Linux", "LX", "#fcc624"),
            new("Visual Studio", "VS", "#5c2d91"),
        ]),
    ];

    private static readonly Profile Russian = new(
        FullName: "Созакбай Ельдос Куатович",
        ShortName: "Yeldos",
        Role: "C#/.NET-разработчик",
        Tagline: "Backend · базы данных и интеграции",
        Summary: "5 лет в коммерческой backend-разработке на C# и .NET (с 2021 года). Разрабатываю серверную логику " +
                 "и интеграции с внешними системами, оптимизирую запросы к PostgreSQL и MS SQL Server.",
        Location: "Астана, Казахстан",
        Citizenship: "Казахстан",
        Contacts: Contacts,
        Stats:
        [
            new("5", "Лет опыта"),
            new("25+", "Технологий"),
            new("4", "Сертификата"),
            new("3", "Языка"),
        ],
        SkillGroups: SkillGroups("Языки", "Платформа и подходы", "Данные и инструменты", "ООП / SOLID"),
        Expertise:
        [
            new("api", "Серверная разработка и интеграции", "Backend",
                "Бизнес-логика и API на .NET, интеграции с внешними системами по SOAP и через Apache Kafka, " +
                "поиск и исправление ошибок на продакшене.",
                ["REST", "SOAP", "Kafka"]),
            new("db", "Базы данных и производительность", "Data",
                "Структура таблиц и индексы, поиск и оптимизация медленных запросов по планам выполнения " +
                "в PostgreSQL и MS SQL Server.",
                ["PostgreSQL", "MS SQL", "SQL"]),
            new("bolt", "Асинхронность и надёжность", "Async",
                "async/await и фоновая обработка, чтобы интерфейс и сервисы не блокировались; повторные попытки " +
                "и журналирование при обмене данными.",
                ["async/await", "Kafka", "Docker"]),
        ],
        Cases:
        [
            new("Ускорение отчётов и запросов к базе данных",
                "Ключевые отчёты и экраны системы открывались слишком долго, часть запросов падала по таймауту " +
                "на больших объёмах данных. Пользователи жаловались, нагрузка на сервер БД росла.",
                [
                    "Проанализировал медленные запросы через планы выполнения (EXPLAIN ANALYZE в PostgreSQL)",
                    "Переписал тяжёлые запросы: убрал лишние JOIN и выборку ненужных столбцов, заменил курсоры и циклы на set-based операции",
                    "Добавил недостающие и составные индексы, убрал неиспользуемые",
                    "Перевёл долгие операции на асинхронную обработку (async/await), чтобы интерфейс не блокировался",
                ],
                ["C#", ".NET", "PostgreSQL"],
                "Время формирования ключевых отчётов сократилось с 15 до 2 секунд, таймауты исчезли, " +
                "нагрузка на сервер БД снизилась на 40%."),
            new("Асинхронная интеграция с внешними системами",
                "Системе нужно было обмениваться данными с несколькими внешними сервисами и смежными системами. " +
                "Синхронные вызовы тормозили основной процесс, а при недоступности внешней стороны данные терялись " +
                "или приходилось переотправлять их вручную.",
                [
                    "Участвовал в проектировании и разработал интеграционный слой: SOAP для внешних API, Apache Kafka для асинхронного обмена между сервисами",
                    "Реализовал повторные попытки, обработку ошибок и журналирование, чтобы сбой на одной стороне не приводил к потере данных",
                    "Работал с контрактами SOAP-сервисов по WSDL, упаковывал сервисы в Docker, участвовал в настройке сборки и выкладки через CI/CD",
                ],
                ["C#", ".NET", "SOAP", "WSDL", "Apache Kafka", "Docker", "CI/CD"],
                "Подключено 10+ внешних систем, обрабатываются тысячи сообщений в сутки, ручная переотправка данных " +
                "больше не нужна. Основной процесс больше не ждёт ответа внешних систем и не зависит от их доступности."),
            new("Клиент-серверный модуль с отчётностью для бизнеса",
                "Бизнесу нужен был новый модуль для работы с данными и выпуска печатных форм и отчётов. " +
                "Раньше отчёты собирали вручную в Excel.",
                [
                    "Вместе с командой собрал требования напрямую с заказчиком, оценил сроки и трудозатраты, презентовал решение",
                    "Реализовал серверную бизнес-логику на .NET",
                    "Разработал структуру БД под модуль",
                    "Сделал отчёты и печатные формы в StimulSoft Reports",
                    "Покрыл ключевую логику unit-тестами, код прошёл code review. В разработке участвовали стажёры, которых я курировал",
                ],
                ["C#", ".NET", "Windows Forms", "StimulSoft Reports", "PostgreSQL", "Unit Testing"],
                "Модуль запущен за пару месяцев, отчёты формируются автоматически вместо ручной сборки, что экономит " +
                "десятки часов в неделю. Модулем пользуются тысячи сотрудников."),
        ],
        Experience:
        [
            new(".NET разработчик", "PBSOFT", "https://pbsoft.kz/",
                "июн 2021 — мар 2026", "4 года 10 месяцев", "Астана",
                "Разработка и сопровождение серверных приложений и сервисов на C# и .NET: бизнес-логика, " +
                "работа с БД, интеграции с внешними системами.",
                [
                    "Реализация бизнес-логики и клиент-серверных модулей по техническим требованиям",
                    "Разработка и оптимизация структуры БД и SQL-запросов (PostgreSQL / MS SQL Server)",
                    "Интеграция с внешними API и сторонними системами (SOAP, Apache Kafka)",
                    "Асинхронная обработка (async/await), чтобы система не блокировалась на долгих операциях",
                    "Участие в code review, следование стандартам чистого кода",
                    "Менторинг и адаптация стажёров",
                    "Оценка задач по времени и сложности, прогноз затрат ресурсов",
                    "Прямое взаимодействие с заказчиками: требования и презентация решений",
                ],
                ["C#", ".NET", "PostgreSQL", "MS SQL", "Kafka", "Agile/Scrum"]),
        ],
        Education:
        [
            new("2024", "Бакалавр · Прикладная информатика",
                "Сибирский институт бизнеса и информационных технологий",
                "Омск · Прикладная информатика в экономике"),
            new("2025", "The Complete Xamarin Developer Course",
                "Udemy", "Повышение квалификации, курсы"),
            new("2025", "PostgreSQL Certification Course",
                "W3Schools", "Повышение квалификации, курсы"),
            new("2020", "Разработка программного обеспечения",
                "Академия ШАГ", "Повышение квалификации, курсы"),
        ],
        Certificates:
        [
            new("2025", "Xamarin Developer: iOS & Android", "Udemy"),
            new("2025", "PostgreSQL Certification Exam", "W3Schools"),
            new("2019", "CCNA: Networking", "Cisco"),
            new("2019", "CCST: Cybersecurity", "Cisco"),
        ],
        Principles:
        [
            new("Качество кода",
                "Участвую в code review и придерживаюсь ООП, SOLID и Clean Code — код должен читаться " +
                "так же легко, как пишется.", "CR", "#6366f1"),
            new("Менторинг",
                "Адаптирую стажёров и помогаю им прокачивать технические навыки — сильная команда важнее " +
                "одного сильного разработчика.", "MT", "#8b5cf6"),
            new("Работа с заказчиком",
                "Общаюсь с заказчиком напрямую: уточняю требования, показываю решения и держу фокус " +
                "на результате при сжатых сроках.", "PM", "#ec4899"),
        ],
        Languages:
        [
            new("Казахский", "Родной", "KZ", 100),
            new("Русский", "C1 — продвинутый", "RU", 90),
            new("Английский", "B2 — выше среднего", "EN", 70),
        ],
        WorkPreferences: "Ищу позицию .NET backend-разработчика (Middle): полная или частичная занятость, " +
                         "проектная работа. Офис, гибрид или удалёнка; готов к переезду и командировкам.");

    private static readonly Profile English = new(
        FullName: "Yeldos Sozakbay",
        ShortName: "Yeldos",
        Role: "C#/.NET Developer",
        Tagline: "Backend · databases & integrations",
        Summary: "5 years of commercial backend development in C# and .NET (since 2021). I build server-side logic " +
                 "and integrations with external systems, and optimize queries in PostgreSQL and MS SQL Server.",
        Location: "Astana, Kazakhstan",
        Citizenship: "Kazakhstan",
        Contacts: Contacts,
        Stats:
        [
            new("5", "Years of experience"),
            new("25+", "Technologies"),
            new("4", "Certificates"),
            new("3", "Languages"),
        ],
        SkillGroups: SkillGroups("Languages", "Platform & practices", "Data & tools", "OOP / SOLID"),
        Expertise:
        [
            new("api", "Backend & Integrations", "Backend",
                "Business logic and APIs in .NET, integrations with external systems via SOAP and Apache Kafka, " +
                "finding and fixing production issues.",
                ["REST", "SOAP", "Kafka"]),
            new("db", "Databases & Performance", "Data",
                "Table design and indexes, finding and optimizing slow queries using execution plans " +
                "in PostgreSQL and MS SQL Server.",
                ["PostgreSQL", "MS SQL", "SQL"]),
            new("bolt", "Async & Reliability", "Async",
                "async/await and background processing so the UI and services never block; retries " +
                "and logging for data exchange.",
                ["async/await", "Kafka", "Docker"]),
        ],
        Cases:
        [
            new("Speeding up reports and database queries",
                "Key reports and screens took too long to load, and some queries timed out on large data volumes. " +
                "Users complained, and the database server load kept growing.",
                [
                    "Analyzed slow queries using execution plans (EXPLAIN ANALYZE in PostgreSQL)",
                    "Rewrote heavy queries: removed redundant JOINs and unnecessary columns, replaced cursors and loops with set-based operations",
                    "Added missing and composite indexes, dropped unused ones",
                    "Moved long-running operations to asynchronous processing (async/await) so the UI stays responsive",
                ],
                ["C#", ".NET", "PostgreSQL"],
                "Key report generation time dropped from 15 to 2 seconds, timeouts disappeared, " +
                "and database server load decreased by 40%."),
            new("Asynchronous integration with external systems",
                "The system had to exchange data with several external services and adjacent systems. " +
                "Synchronous calls slowed down the main workflow, and when the other side was unavailable, " +
                "data was lost or had to be resent manually.",
                [
                    "Took part in designing and built the integration layer: SOAP for external APIs, Apache Kafka for asynchronous messaging between services",
                    "Implemented retries, error handling and logging, so a failure on one side no longer caused data loss",
                    "Worked with SOAP service contracts via WSDL, containerized services with Docker, and helped set up build and deployment via CI/CD",
                ],
                ["C#", ".NET", "SOAP", "WSDL", "Apache Kafka", "Docker", "CI/CD"],
                "10+ external systems connected, thousands of messages processed daily, and manual resending is " +
                "no longer needed. The main workflow no longer waits for external systems and doesn't depend on their availability."),
            new("Client-server module with business reporting",
                "The business needed a new module for working with data and generating printable forms and reports. " +
                "Previously, reports were compiled manually in Excel.",
                [
                    "Gathered requirements directly from the client together with the team, estimated timelines and effort, and presented the solution",
                    "Implemented the server-side business logic in .NET",
                    "Built the database schema for the module",
                    "Built reports and printable forms in StimulSoft Reports",
                    "Covered the core logic with unit tests; all code went through code review. Interns I supervised took part in development",
                ],
                ["C#", ".NET", "Windows Forms", "StimulSoft Reports", "PostgreSQL", "Unit Testing"],
                "The module was launched in a couple of months; reports are now generated automatically instead of " +
                "being compiled by hand, saving dozens of hours per week. The module is used by thousands of employees."),
        ],
        Experience:
        [
            new(".NET Developer", "PBSOFT", "https://pbsoft.kz/",
                "Jun 2021 — Mar 2026", "4 years 10 months", "Astana",
                "Developing and maintaining server-side applications and services in C# and .NET: business logic, " +
                "database work and integrations with external systems.",
                [
                    "Implemented business logic and client-server modules to technical specifications",
                    "Built and optimized database schemas and SQL queries (PostgreSQL / MS SQL Server)",
                    "Integrated external APIs and third-party systems (SOAP, Apache Kafka)",
                    "Used async/await so the system doesn't block on long-running operations",
                    "Took part in code reviews and followed clean code standards",
                    "Mentored and onboarded interns",
                    "Estimated task effort and complexity, forecast development resources",
                    "Worked directly with clients: gathering requirements and presenting solutions",
                ],
                ["C#", ".NET", "PostgreSQL", "MS SQL", "Kafka", "Agile/Scrum"]),
        ],
        Education:
        [
            new("2024", "Bachelor's · Applied Informatics",
                "Siberian Institute of Business and Information Technologies",
                "Omsk · Applied Informatics in Economics"),
            new("2025", "The Complete Xamarin Developer Course",
                "Udemy", "Professional development course"),
            new("2025", "PostgreSQL Certification Course",
                "W3Schools", "Professional development course"),
            new("2020", "Software Development",
                "IT STEP Academy", "Professional development course"),
        ],
        Certificates:
        [
            new("2025", "Xamarin Developer: iOS & Android", "Udemy"),
            new("2025", "PostgreSQL Certification Exam", "W3Schools"),
            new("2019", "CCNA: Networking", "Cisco"),
            new("2019", "CCST: Cybersecurity", "Cisco"),
        ],
        Principles:
        [
            new("Code quality",
                "I take part in code reviews and stick to OOP, SOLID and Clean Code — code should be " +
                "as easy to read as it is to write.", "CR", "#6366f1"),
            new("Mentoring",
                "I onboard interns and help them grow their technical skills — a strong team matters more " +
                "than one strong developer.", "MT", "#8b5cf6"),
            new("Working with clients",
                "I talk to clients directly: clarify requirements, show solutions and stay focused on results " +
                "under tight deadlines.", "PM", "#ec4899"),
        ],
        Languages:
        [
            new("Kazakh", "Native", "KZ", 100),
            new("Russian", "C1 — Advanced", "RU", 90),
            new("English", "B2 — Upper-intermediate", "EN", 70),
        ],
        WorkPreferences: "Looking for a .NET backend developer role (Middle): full-time, part-time or project work. " +
                         "On-site, hybrid or fully remote; ready to relocate and travel.");

    private static readonly Profile Kazakh = new(
        FullName: "Созакбай Ельдос Куатович",
        ShortName: "Yeldos",
        Role: "C#/.NET әзірлеушісі",
        Tagline: "Backend · дерекқорлар және интеграциялар",
        Summary: "C# және .NET платформасындағы коммерциялық backend-әзірлеуде 5 жылдық тәжірибем бар (2021 жылдан бері). " +
                 "Серверлік логиканы және сыртқы жүйелермен интеграцияларды әзірлеймін, PostgreSQL мен MS SQL Server-дегі " +
                 "сұраныстарды оңтайландырамын.",
        Location: "Астана, Қазақстан",
        Citizenship: "Қазақстан",
        Contacts: Contacts,
        Stats:
        [
            new("5", "Жыл тәжірибе"),
            new("25+", "Технология"),
            new("4", "Сертификат"),
            new("3", "Тіл"),
        ],
        SkillGroups: SkillGroups("Тілдер", "Платформа және тәсілдер", "Деректер және құралдар", "OOP / SOLID"),
        Expertise:
        [
            new("api", "Серверлік әзірлеу және интеграциялар", "Backend",
                ".NET-те бизнес-логика мен API, SOAP және Apache Kafka арқылы сыртқы жүйелермен интеграция, " +
                "продакшендегі қателерді табу және түзету.",
                ["REST", "SOAP", "Kafka"]),
            new("db", "Дерекқорлар және өнімділік", "Data",
                "Кесте құрылымы мен индекстер, PostgreSQL және MS SQL Server-де орындалу жоспарлары бойынша " +
                "баяу сұраныстарды табу және оңтайландыру.",
                ["PostgreSQL", "MS SQL", "SQL"]),
            new("bolt", "Асинхрондылық және сенімділік", "Async",
                "Интерфейс пен сервистер бұғатталмауы үшін async/await және фондық өңдеу; деректер алмасу кезінде " +
                "қайта жіберу және журналдау.",
                ["async/await", "Kafka", "Docker"]),
        ],
        Cases:
        [
            new("Есептер мен дерекқор сұраныстарын жылдамдату",
                "Жүйенің негізгі есептері мен экрандары тым ұзақ ашылатын, үлкен көлемді деректерде кейбір сұраныстар " +
                "таймаутқа түсетін. Пайдаланушылар шағымданып, дерекқор серверіне жүктеме өсті.",
                [
                    "Баяу сұраныстарды орындалу жоспарлары арқылы талдадым (PostgreSQL-де EXPLAIN ANALYZE)",
                    "Ауыр сұраныстарды қайта жаздым: артық JOIN-дар мен қажетсіз бағандарды алып тастадым, курсорлар мен циклдерді set-based операциялармен алмастырдым",
                    "Жетіспейтін және құрама индекстерді қостым, пайдаланылмайтындарын алып тастадым",
                    "Интерфейс бұғатталмауы үшін ұзақ операцияларды асинхронды өңдеуге (async/await) көшірдім",
                ],
                ["C#", ".NET", "PostgreSQL"],
                "Негізгі есептерді қалыптастыру уақыты 15 секундтан 2 секундқа дейін қысқарды, таймауттар жойылды, " +
                "дерекқор серверіне жүктеме 40%-ға төмендеді."),
            new("Сыртқы жүйелермен асинхронды интеграция",
                "Жүйеге бірнеше сыртқы сервистермен және іргелес жүйелермен деректер алмасу қажет болды. " +
                "Синхронды шақырулар негізгі процесті баяулататын, ал сыртқы тарап қолжетімсіз болғанда деректер " +
                "жоғалатын немесе оларды қолмен қайта жіберуге тура келетін.",
                [
                    "Интеграциялық қабатты жобалауға қатысып, оны әзірледім: сыртқы API үшін SOAP, сервистер арасындағы асинхронды алмасу үшін Apache Kafka",
                    "Бір тараптағы ақау деректердің жоғалуына әкелмеуі үшін қайта жіберуді, қателерді өңдеуді және журналдауды іске асырдым",
                    "SOAP-сервистердің WSDL келісімшарттарымен жұмыс істедім, сервистерді Docker-ге орадым, CI/CD арқылы құрастыру мен жариялауды баптауға қатыстым",
                ],
                ["C#", ".NET", "SOAP", "WSDL", "Apache Kafka", "Docker", "CI/CD"],
                "10-нан астам сыртқы жүйе қосылды, тәулігіне мыңдаған хабарлама өңделеді, деректерді қолмен қайта " +
                "жіберу енді қажет емес. Негізгі процесс сыртқы жүйелердің жауабын күтпейді және олардың қолжетімділігіне тәуелді емес."),
            new("Бизнеске арналған есептілігі бар клиент-серверлік модуль",
                "Бизнеске деректермен жұмыс істеуге және баспа формалары мен есептерді шығаруға арналған жаңа модуль " +
                "қажет болды. Бұрын есептер Excel-де қолмен жиналатын.",
                [
                    "Командамен бірге талаптарды тікелей тапсырыс берушіден жинадым, мерзімдер мен еңбек шығынын бағалап, шешімді таныстырдым",
                    ".NET-те серверлік бизнес-логиканы іске асырдым",
                    "Модульге арналған дерекқор құрылымын әзірледім",
                    "StimulSoft Reports-та есептер мен баспа формаларын жасадым",
                    "Негізгі логиканы unit-тесттермен қамтыдым, код code review-дан өтті. Әзірлеуге мен жетекшілік еткен тағылымдамашылар қатысты",
                ],
                ["C#", ".NET", "Windows Forms", "StimulSoft Reports", "PostgreSQL", "Unit Testing"],
                "Модуль бірнеше айда іске қосылды, есептер қолмен жиналудың орнына автоматты түрде қалыптасады — " +
                "бұл аптасына ондаған сағат үнемдейді. Модульді мыңдаған қызметкер пайдаланады."),
        ],
        Experience:
        [
            new(".NET әзірлеушісі", "PBSOFT", "https://pbsoft.kz/",
                "маус. 2021 — нау. 2026", "4 жыл 10 ай", "Астана",
                "C# және .NET-те серверлік қосымшалар мен сервистерді әзірлеу және сүйемелдеу: бизнес-логика, " +
                "дерекқормен жұмыс, сыртқы жүйелермен интеграция.",
                [
                    "Техникалық талаптар бойынша бизнес-логика мен клиент-серверлік модульдерді іске асыру",
                    "Дерекқор құрылымы мен SQL-сұраныстарды әзірлеу және оңтайландыру (PostgreSQL / MS SQL Server)",
                    "Сыртқы API және үшінші тарап жүйелерімен интеграция (SOAP, Apache Kafka)",
                    "Жүйе ұзақ операцияларда бұғатталмауы үшін асинхронды өңдеу (async/await)",
                    "Code review-ға қатысу, таза код стандарттарын ұстану",
                    "Тағылымдамашыларға тәлімгерлік ету және оларды бейімдеу",
                    "Тапсырмаларды уақыты мен күрделілігі бойынша бағалау, ресурс шығынын болжау",
                    "Тапсырыс берушілермен тікелей жұмыс: талаптарды нақтылау және шешімдерді таныстыру",
                ],
                ["C#", ".NET", "PostgreSQL", "MS SQL", "Kafka", "Agile/Scrum"]),
        ],
        Education:
        [
            new("2024", "Бакалавр · Қолданбалы информатика",
                "Сібір бизнес және ақпараттық технологиялар институты",
                "Омбы · Экономикадағы қолданбалы информатика"),
            new("2025", "The Complete Xamarin Developer Course",
                "Udemy", "Біліктілікті арттыру курсы"),
            new("2025", "PostgreSQL Certification Course",
                "W3Schools", "Біліктілікті арттыру курсы"),
            new("2020", "Бағдарламалық қамтамасыз етуді әзірлеу",
                "ШАГ академиясы", "Біліктілікті арттыру курсы"),
        ],
        Certificates:
        [
            new("2025", "Xamarin Developer: iOS & Android", "Udemy"),
            new("2025", "PostgreSQL Certification Exam", "W3Schools"),
            new("2019", "CCNA: Networking", "Cisco"),
            new("2019", "CCST: Cybersecurity", "Cisco"),
        ],
        Principles:
        [
            new("Код сапасы",
                "Code review-ға қатысамын және OOP, SOLID, Clean Code қағидаттарын ұстанамын — код қалай оңай " +
                "жазылса, солай оңай оқылуы керек.", "CR", "#6366f1"),
            new("Тәлімгерлік",
                "Тағылымдамашыларды бейімдеп, олардың техникалық дағдыларын дамытуға көмектесемін — күшті команда " +
                "бір күшті әзірлеушіден маңыздырақ.", "MT", "#8b5cf6"),
            new("Тапсырыс берушімен жұмыс",
                "Тапсырыс берушімен тікелей сөйлесемін: талаптарды нақтылаймын, шешімдерді көрсетемін және " +
                "қысқа мерзімде нәтижеге назар аударамын.", "PM", "#ec4899"),
        ],
        Languages:
        [
            new("Қазақ тілі", "Ана тілі", "KZ", 100),
            new("Орыс тілі", "C1 — жоғары деңгей", "RU", 90),
            new("Ағылшын тілі", "B2 — орташадан жоғары", "EN", 70),
        ],
        WorkPreferences: ".NET backend-әзірлеуші (Middle) лауазымын іздеймін: толық немесе жартылай жұмыс күні, " +
                         "жобалық жұмыс. Кеңсе, гибрид немесе қашықтан; басқа қалаға көшуге және іссапарларға дайынмын.");
}
