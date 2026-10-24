using Portfolio.Models;

namespace Portfolio.Services;

public static class ProfileData
{

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

    public static string CvPath(string lang) => $"/files/Yeldos-Sozakbay-CV-{Normalize(lang)}.pdf";

    public const string DisplayName = "Yeldos Sozakbay";

    public const string SiteUrl = "https://sozakbay.dev";

    private static readonly string[] PayLedgerTags =
        ["Java 21", "Spring Boot 4", "Spring Data JPA", "PostgreSQL", "Flyway", "Maven", "Kafka", "Docker"];

    private static readonly string[] AnikiTags = ["C#", ".NET 10", "Worker Service", "Telegram Bot API", "xUnit"];

    private static readonly string[] OrderFlowTags =
        ["ASP.NET Core", "EF Core", "PostgreSQL", "Kafka", "Outbox", "Polly", "xUnit", "Testcontainers", "Serilog", "Docker"];

    private static IReadOnlyList<SkillGroup> SkillGroups(string languages, string platform, string data, string oop) =>
    [
        new(languages,
        [
            "C#",
            "SQL",
            "JavaScript",
            "Python",
            "Java",
        ]),
        new(platform,
        [
            ".NET",
            "ASP.NET Core",
            "Web API",
            "Entity Framework Core",
            "LINQ",
            "MVC",
            "REST",
            "SOAP",
            oop,
            "xUnit",
            "Windows Forms",
            ".NET MAUI",
            ".NET Framework",
            "WPF",
            "gRPC",
            "DDD",
            "Spring Boot",
            "Spring Data JPA",
            "Hibernate",
            "JUnit 5",
        ]),
        new(data,
        [
            "PostgreSQL",
            "MS SQL Server",
            "Redis",
            "Apache Kafka",
            "Docker",
            "Git",
            "CI/CD",
            "StimulSoft",
            "MySQL",
            "MongoDB",
            "ClickHouse",
            "RabbitMQ",
            "Kubernetes",
            "Grafana",
            "Maven",
            "Flyway",
            "Testcontainers",
        ]),
    ];

    private static readonly Profile Russian = new(
        FullName: "Созакбай Ельдос Куатович",
        Role: "C#/.NET-разработчик",
        Summary: "5 лет в коммерческой backend-разработке на C# и .NET (с 2021 года). Разрабатываю серверную логику " +
                 "и интеграции с внешними системами, оптимизирую запросы к PostgreSQL и MS SQL Server. " +
                 "С апреля 2026 работаю на фрилансе: частные заказы и пет-проекты.",
        Location: "Астана, Казахстан",
        Contacts: Contacts,
        Stats:
        [
            new("5", "Лет опыта"),
            new("40+", "Технологий"),
            new("4", "Сертификата"),
            new("3", "Языка"),
        ],
        SkillGroups: SkillGroups("Языки", "Платформа и подходы", "Данные и инструменты", "ООП / SOLID"),
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
        Projects:
        [
            new("PayLedger — платёжный сервис с двойной записью",
                "Финтех-проект, счета, переводы и проводки по принципу двойной записи.",
                [
                    "REST API на Spring Boot 4",
                    "Двойная запись: каждый перевод — сбалансированные проводки в одной транзакции",
                    "Защита от двойной траты: optimistic locking и повтор при конфликте",
                    "Идемпотентность переводов по заголовку Idempotency-Key",
                    "Структурированные JSON-логи, PostgreSQL и Flyway-миграции",
                    "Дальше: Transactional Outbox → Kafka, тесты на Testcontainers, CI",
                ],
                PayLedgerTags,
                "https://github.com/yeldos13/PayLedger"),
            new("OrderFlow — сервис заказов с надёжной доставкой событий",
                "Сервис заказов, который передаёт события во внешнюю систему через Kafka и переживает её сбои.",
                [
                    "REST API на ASP.NET Core: ProblemDetails, OpenAPI, health checks",
                    "Transactional Outbox: заказ и событие в одной транзакции",
                    "Идемпотентность создания заказа по ключу запроса",
                    "Kafka consumer с дедупликацией повторных событий",
                    "Polly: повторы и circuit breaker, dead letter topic",
                    "Unit-тесты и интеграционные тесты на Testcontainers",
                ],
                OrderFlowTags,
                "https://github.com/yeldos13/OrderFlow"),
            new("AnikiChatBot — Telegram-бот для групповых чатов",
                "Telegram-бот на .NET 10 (Worker Service) с полезными командами для чатов.",
                [
                    "Курсы валют и погода по команде",
                    "Антиспам для групп",
                    "Статистика активности чата",
                    "Unit-тесты на xUnit",
                ],
                AnikiTags,
                "https://github.com/yeldos13/TelegramBot"),
        ],
        Experience:
        [
            new(".NET разработчик", "PBSOFT", "https://pbsoft.kz/",
                "июн 2021 — мар 2026", "4 года 10 месяцев",
                "Разработка и сопровождение серверных приложений и сервисов на C# и .NET: бизнес-логика, " +
                "работа с БД, интеграции с внешними системами.",
                [
                    "REST API на ASP.NET Core (Web API) с документацией в Swagger",
                    "Работа с данными через Entity Framework Core и LINQ, прямые SQL-запросы там, где важна производительность",
                    "Проектирование и оптимизация структуры БД и SQL-запросов (PostgreSQL, MS SQL Server), кэширование в Redis",
                    "Интеграция с внешними API и сторонними системами (SOAP, Apache Kafka), упаковка сервисов в Docker, сборка и выкладка через CI/CD",
                    "Unit-тесты бизнес-логики на xUnit, асинхронная обработка (async/await)",
                    "Поиск и исправление ошибок на продакшене, сопровождение действующих сервисов",
                    "Оценка задач, работа по Agile/Scrum, прямое общение с заказчиками для уточнения требований и презентации решений",
                    "Курировал стажёров, участвовал в code review",
                ],
                ["C#", ".NET", "ASP.NET Core", "EF Core", "PostgreSQL", "MS SQL", "Redis", "Kafka", "xUnit", "Agile/Scrum"]),
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
        WorkPreferences: "Ищу позицию .NET backend-разработчика: полная или частичная занятость, " +
                         "проектная работа. Офис, гибрид или удалёнка; готов к переезду и командировкам.");

    private static readonly Profile English = new(
        FullName: "Yeldos Sozakbay",
        Role: "C#/.NET Developer",
        Summary: "5 years of commercial backend development in C# and .NET (since 2021). I build server-side logic " +
                 "and integrations with external systems, and optimize queries in PostgreSQL and MS SQL Server. " +
                 "Since April 2026 I've been freelancing: client projects and side projects.",
        Location: "Astana, Kazakhstan",
        Contacts: Contacts,
        Stats:
        [
            new("5", "Years of experience"),
            new("40+", "Technologies"),
            new("4", "Certificates"),
            new("3", "Languages"),
        ],
        SkillGroups: SkillGroups("Languages", "Platform & practices", "Data & tools", "OOP / SOLID"),
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
        Projects:
        [
            new("PayLedger — double-entry payment ledger",
                "A fintech project: accounts, transfers and double-entry postings.",
                [
                    "REST API on Spring Boot 4: Problem Details, OpenAPI, health checks",
                    "Double-entry: every transfer is a set of balanced postings in one transaction",
                    "No double spending: optimistic locking with retry on conflict",
                    "Idempotent transfers via the Idempotency-Key header",
                    "Structured JSON logs, PostgreSQL and Flyway migrations",
                    "Next: Transactional Outbox → Kafka, Testcontainers tests, CI",
                ],
                PayLedgerTags,
                "https://github.com/yeldos13/PayLedger"),
            new("OrderFlow — order service with reliable event delivery",
                "An order service that delivers events to an external system via Kafka and survives its failures.",
                [
                    "REST API on ASP.NET Core: ProblemDetails, OpenAPI, health checks",
                    "Transactional Outbox: order and event saved in one transaction",
                    "Idempotent order creation via request key",
                    "Kafka consumer with deduplication of redelivered events",
                    "Polly: retries and circuit breaker, dead letter topic",
                    "Unit tests and integration tests with Testcontainers",
                ],
                OrderFlowTags,
                "https://github.com/yeldos13/OrderFlow"),
            new("AnikiChatBot — Telegram bot for group chats",
                "A Telegram bot on .NET 10 (Worker Service) with handy commands for chats.",
                [
                    "Exchange rates and weather on command",
                    "Anti-spam for groups",
                    "Chat activity statistics",
                    "Unit tests with xUnit",
                ],
                AnikiTags,
                "https://github.com/yeldos13/TelegramBot"),
        ],
        Experience:
        [
            new(".NET Developer", "PBSOFT", "https://pbsoft.kz/",
                "Jun 2021 — Mar 2026", "4 years 10 months",
                "Developing and maintaining server-side applications and services in C# and .NET: business logic, " +
                "database work and integrations with external systems.",
                [
                    "REST API on ASP.NET Core (Web API) documented with Swagger",
                    "Data access with Entity Framework Core and LINQ, raw SQL where performance matters",
                    "Designed and optimized database schemas and SQL queries (PostgreSQL, MS SQL Server), caching in Redis",
                    "Integrated external APIs and third-party systems (SOAP, Apache Kafka), packaged services in Docker, build and deployment via CI/CD",
                    "Unit tests for business logic with xUnit, async processing (async/await)",
                    "Found and fixed production issues, maintained live services",
                    "Task estimation, Agile/Scrum, direct communication with clients to clarify requirements and present solutions",
                    "Supervised interns and took part in code reviews",
                ],
                ["C#", ".NET", "ASP.NET Core", "EF Core", "PostgreSQL", "MS SQL", "Redis", "Kafka", "xUnit", "Agile/Scrum"]),
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
        WorkPreferences: "Looking for a .NET backend developer role: full-time, part-time or project work. " +
                         "On-site, hybrid or fully remote; ready to relocate and travel.");

    private static readonly Profile Kazakh = new(
        FullName: "Созакбай Ельдос Куатович",
        Role: "C#/.NET әзірлеушісі",
        Summary: "C# және .NET платформасындағы коммерциялық backend-әзірлеуде 5 жылдық тәжірибем бар (2021 жылдан бері). " +
                 "Серверлік логиканы және сыртқы жүйелермен интеграцияларды әзірлеймін, PostgreSQL мен MS SQL Server-дегі " +
                 "сұраныстарды оңтайландырамын. 2026 жылдың сәуірінен бастап фрилансте жұмыс істеймін: жеке тапсырыстар мен жеке жобалар.",
        Location: "Астана, Қазақстан",
        Contacts: Contacts,
        Stats:
        [
            new("5", "Жыл тәжірибе"),
            new("40+", "Технология"),
            new("4", "Сертификат"),
            new("3", "Тіл"),
        ],
        SkillGroups: SkillGroups("Тілдер", "Платформа және тәсілдер", "Деректер және құралдар", "OOP / SOLID"),
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
        Projects:
        [
            new("PayLedger — қос жазбалы төлем сервисі",
                "Финтех-жоба: шоттар, аударымдар және қос жазба қағидасы бойынша проводкалар.",
                [
                    "Spring Boot 4-тегі REST API: Problem Details, OpenAPI, health checks",
                    "Қос жазба: әр аударым — бір транзакциядағы теңгерілген проводкалар",
                    "Екі рет жұмсаудан қорғау: optimistic locking және қақтығыс кезінде қайталау",
                    "Idempotency-Key тақырыбы бойынша аударымдардың идемпотенттілігі",
                    "Құрылымдалған JSON-логтар, PostgreSQL және Flyway-миграциялар",
                    "Әрі қарай: Transactional Outbox → Kafka, Testcontainers тесттері, CI",
                ],
                PayLedgerTags,
                "https://github.com/yeldos13/PayLedger"),
            new("OrderFlow — оқиғаларды сенімді жеткізетін тапсырыстар сервисі",
                "Оқиғаларды Kafka арқылы сыртқы жүйеге жіберетін және оның ақауларына төзімді тапсырыстар сервисі.",
                [
                    "ASP.NET Core-дағы REST API: ProblemDetails, OpenAPI, health checks",
                    "Transactional Outbox: тапсырыс пен оқиға бір транзакцияда",
                    "Сұраныс кілті бойынша тапсырыс құрудың идемпотенттілігі",
                    "Қайталанған оқиғаларды дедупликациялайтын Kafka consumer",
                    "Polly: қайталау және circuit breaker, dead letter topic",
                    "Unit-тесттер және Testcontainers-тегі интеграциялық тесттер",
                ],
                OrderFlowTags,
                "https://github.com/yeldos13/OrderFlow"),
            new("AnikiChatBot — топтық чаттарға арналған Telegram-бот",
                "Чаттарға пайдалы командалары бар .NET 10 (Worker Service) Telegram-боты.",
                [
                    "Команда бойынша валюта бағамы және ауа райы",
                    "Топтарға арналған антиспам",
                    "Чат белсенділігінің статистикасы",
                    "xUnit-тегі unit-тесттер",
                ],
                AnikiTags,
                "https://github.com/yeldos13/TelegramBot"),
        ],
        Experience:
        [
            new(".NET әзірлеушісі", "PBSOFT", "https://pbsoft.kz/",
                "маус. 2021 — нау. 2026", "4 жыл 10 ай",
                "C# және .NET-те серверлік қосымшалар мен сервистерді әзірлеу және сүйемелдеу: бизнес-логика, " +
                "дерекқормен жұмыс, сыртқы жүйелермен интеграция.",
                [
                    "ASP.NET Core (Web API)-да Swagger құжаттамасы бар REST API",
                    "Entity Framework Core және LINQ арқылы деректермен жұмыс, өнімділік маңызды жерде тікелей SQL-сұраныстар",
                    "Дерекқор құрылымы мен SQL-сұраныстарды жобалау және оңтайландыру (PostgreSQL, MS SQL Server), Redis-те кэштеу",
                    "Сыртқы API және үшінші тарап жүйелерімен интеграция (SOAP, Apache Kafka), сервистерді Docker-ге орау, CI/CD арқылы құрастыру және жариялау",
                    "xUnit-тегі бизнес-логиканың unit-тесттері, асинхронды өңдеу (async/await)",
                    "Продакшендегі қателерді іздеу және түзету, жұмыс істеп тұрған сервистерді сүйемелдеу",
                    "Тапсырмаларды бағалау, Agile/Scrum бойынша жұмыс, талаптарды нақтылау және шешімдерді таныстыру үшін тапсырыс берушілермен тікелей байланыс",
                    "Тағылымдамашыларға жетекшілік еттім, code review-ға қатыстым",
                ],
                ["C#", ".NET", "ASP.NET Core", "EF Core", "PostgreSQL", "MS SQL", "Redis", "Kafka", "xUnit", "Agile/Scrum"]),
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
        WorkPreferences: ".NET backend-әзірлеуші лауазымын іздеймін: толық немесе жартылай жұмыс күні, " +
                         "жобалық жұмыс. Кеңсе, гибрид немесе қашықтан; басқа қалаға көшуге және іссапарларға дайынмын.");
}
