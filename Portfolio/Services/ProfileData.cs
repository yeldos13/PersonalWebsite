using Portfolio.Models;

namespace Portfolio.Services;

public static class ProfileData
{
    public static readonly string[] Languages = ["ru", "en"];

    public static string Normalize(string? lang) =>
        string.Equals(lang, "en", StringComparison.OrdinalIgnoreCase) ? "en" : "ru";

    public static Profile Get(string? lang) => Normalize(lang) == "en" ? English : Russian;

    private static readonly Contacts Contacts = new("@sozakbay", "eldos.sozakbay@gmail.com", "+7 (777) 190-54-80");

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
        Tagline: "Backend · высоконагруженные системы",
        Summary: "Почти 5 лет в коммерческой backend-разработке (с 2021 года). Специализируюсь на высоконагруженных " +
                 "распределённых системах, микросервисной архитектуре и оптимизации производительности на .NET (C#).",
        Location: "Астана, Казахстан",
        Citizenship: "Казахстан",
        Contacts: Contacts,
        Stats:
        [
            new("4", "Года опыта"),
            new("25+", "Технологий"),
            new("4", "Сертификата"),
            new("3", "Языка"),
        ],
        SkillGroups: SkillGroups("Языки", "Платформа и подходы", "Данные и инструменты", "ООП / SOLID"),
        Expertise:
        [
            new("api", "Архитектура и профилирование", "Backend",
                "Проектирование REST/SOAP API, интеграции с внешними системами и микросервисами, " +
                "отладка сложных дефектов на продакшн-средах.",
                ["REST", "SOAP", "Microservices"]),
            new("db", "Базы данных и производительность", "Data",
                "Оптимизация узких мест в запросах и структуре БД, работа с большими объёмами данных " +
                "в PostgreSQL, MS SQL Server и NoSQL.",
                ["PostgreSQL", "MS SQL", "NoSQL"]),
            new("bolt", "Асинхронность и нагрузка", "Highload",
                "Асинхронная обработка на async/await, многопоточность и Apache Kafka для повышения " +
                "отклика высоконагруженных сервисов.",
                ["async/await", "Kafka", "Docker"]),
        ],
        Experience:
        [
            new("Middle .NET разработчик", "PBSOFT", "https://pbsoft.kz/",
                "июн 2021 — мар 2026", "4 года 10 месяцев", "Астана",
                "Разработка высоконагруженных сервисов и приложений на C# и .NET: от проектирования БД " +
                "и бизнес-логики до сопровождения на продакшне.",
                [
                    "Реализация бизнес-логики и клиент-серверных модулей по техническим требованиям",
                    "Проектирование и оптимизация структуры БД (PostgreSQL / MS SQL Server)",
                    "Архитектура и интеграция внешних API, микросервисов и сторонних систем",
                    "Асинхронность (async/await) и многопоточность для повышения отклика системы",
                    "Code review и контроль стандартов чистого кода в команде",
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
                "Провожу code review и держу команду в рамках ООП, SOLID и Clean Code — код должен читаться " +
                "так же легко, как пишется.", "CR", "#6366f1"),
            new("Менторинг",
                "Адаптирую стажёров и помогаю им прокачивать технические навыки — сильная команда важнее " +
                "одного сильного разработчика.", "MT", "#8b5cf6"),
            new("Работа с заказчиком",
                "Сам собираю требования, презентую решения и держу фокус на результате даже при сжатых " +
                "дедлайнах.", "PM", "#ec4899"),
        ],
        Languages:
        [
            new("Казахский", "Родной", "KZ", 100),
            new("Русский", "C1 — продвинутый", "RU", 90),
            new("Английский", "B2 — выше среднего", "EN", 70),
        ],
        WorkPreferences: "Рассматриваю разработку высоконагруженных сервисов и сложных продуктов: полная или частичная " +
                         "занятость, проектная работа, стажировка. Офис, гибрид или удалёнка; готов к переезду и командировкам.");

    private static readonly Profile English = new(
        FullName: "Yeldos Sozakbay",
        ShortName: "Yeldos",
        Role: "C#/.NET Developer",
        Tagline: "Backend · high-load systems",
        Summary: "Nearly 5 years of commercial backend development (since 2021), specializing in high-load " +
                 "distributed systems, microservices architecture and performance optimization on .NET (C#).",
        Location: "Astana, Kazakhstan",
        Citizenship: "Kazakhstan",
        Contacts: Contacts,
        Stats:
        [
            new("4", "Years of experience"),
            new("25+", "Technologies"),
            new("4", "Certificates"),
            new("3", "Languages"),
        ],
        SkillGroups: SkillGroups("Languages", "Platform & practices", "Data & tools", "OOP / SOLID"),
        Expertise:
        [
            new("api", "Architecture & Profiling", "Backend",
                "Designing REST/SOAP APIs, integrating external systems and microservices, " +
                "debugging complex defects in production.",
                ["REST", "SOAP", "Microservices"]),
            new("db", "Databases & Performance", "Data",
                "Removing bottlenecks in queries and database design, working with large volumes of data " +
                "in PostgreSQL, MS SQL Server and NoSQL.",
                ["PostgreSQL", "MS SQL", "NoSQL"]),
            new("bolt", "Async & High Load", "Highload",
                "Asynchronous processing with async/await, multithreading and Apache Kafka to keep " +
                "high-load services responsive.",
                ["async/await", "Kafka", "Docker"]),
        ],
        Experience:
        [
            new("Middle .NET Developer", "PBSOFT", "https://pbsoft.kz/",
                "Jun 2021 — Mar 2026", "4 years 10 months", "Astana",
                "Building high-load services and applications with C# and .NET — from database design " +
                "and business logic to production support.",
                [
                    "Implemented business logic and client-server modules to technical specifications",
                    "Designed and optimized database schemas (PostgreSQL / MS SQL Server)",
                    "Architected and integrated external APIs, microservices and third-party systems",
                    "Used async/await and multithreading to improve system responsiveness",
                    "Ran code reviews and enforced clean code standards across the team",
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
                "I run code reviews and keep the team aligned with OOP, SOLID and Clean Code — code should be " +
                "as easy to read as it is to write.", "CR", "#6366f1"),
            new("Mentoring",
                "I onboard interns and help them grow their technical skills — a strong team matters more " +
                "than one strong developer.", "MT", "#8b5cf6"),
            new("Working with clients",
                "I gather requirements, present solutions and stay focused on results even under tight " +
                "deadlines.", "PM", "#ec4899"),
        ],
        Languages:
        [
            new("Kazakh", "Native", "KZ", 100),
            new("Russian", "C1 — Advanced", "RU", 90),
            new("English", "B2 — Upper-intermediate", "EN", 70),
        ],
        WorkPreferences: "Open to opportunities building high-load services and complex software products: full-time, " +
                         "part-time, project work or internship. On-site, hybrid or fully remote; ready to relocate and travel.");
}
