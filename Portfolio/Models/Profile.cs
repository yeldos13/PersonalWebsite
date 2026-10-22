namespace Portfolio.Models;

public record Profile(
    string FullName,
    string Role,
    string Summary,
    string Location,
    Contacts Contacts,
    IReadOnlyList<Stat> Stats,
    IReadOnlyList<SkillGroup> SkillGroups,
    IReadOnlyList<Expertise> Expertise,
    IReadOnlyList<CaseStudy> Cases,
    IReadOnlyList<Project> Projects,
    IReadOnlyList<Job> Experience,
    IReadOnlyList<Education> Education,
    IReadOnlyList<Certificate> Certificates,
    IReadOnlyList<Principle> Principles,
    IReadOnlyList<SpokenLanguage> Languages,
    string WorkPreferences);

public record Contacts(string Telegram, string Email, string Phone, string GitHubUrl, string LinkedInUrl)
{
    public static string Display(string url) => url.Replace("https://", "").Replace("www.", "").TrimEnd('/');

    public string TelegramUrl => $"https://t.me/{Telegram.TrimStart('@')}";
    public string PhoneUrl => "tel:" + new string(Phone.Where(c => char.IsDigit(c) || c == '+').ToArray());
}

public record Stat(string Value, string Label);

public record SkillGroup(string Title, IReadOnlyList<string> Items);

public record Expertise(string Icon, string Title, string Description, IReadOnlyList<string> Tags);

public record CaseStudy(string Title, string Problem, IReadOnlyList<string> Solution, IReadOnlyList<string> Stack, string Result);

public record Project(string Title, string Description, IReadOnlyList<string> Highlights, IReadOnlyList<string> Tags, string Url);

public record Job(
    string Title,
    string Company,
    string CompanyUrl,
    string Period,
    string Duration,
    string Description,
    IReadOnlyList<string> Highlights,
    IReadOnlyList<string> Tags);

public record Education(string Year, string Degree, string Institution, string Details);

public record Certificate(string Year, string Title, string Issuer);

public record Principle(string Title, string Text, string Initials, string Color);

public record SpokenLanguage(string Name, string Level, string Code, int Percent);
