using Portfolio.Services;

namespace Portfolio.Tests;

public class ParsingTests
{
    // Trimmed from github.com/users/<user>/contributions; if GitHub changes the markup, this is where it shows.
    private const string Calendar = """
        <td tabindex="0" data-ix="0" aria-selected="false" aria-describedby="contribution-graph-legend-level-0" style="width: 10px"
          data-date="2026-11-11" id="contribution-day-component-3-50" data-level="2" role="gridcell" data-view-component="true" class="ContributionCalendar-day"></td>
        <td data-date="2026-11-10" id="contribution-day-component-2-50" data-level="0" role="gridcell" data-view-component="true" class="ContributionCalendar-day"></td>
        <tool-tip style="pointer-events: none;" id="tooltip-1" for="contribution-day-component-2-50" popover="manual" data-direction="n" data-type="label" data-view-component="true" class="sr-only position-absolute">No contributions on November 10th.</tool-tip>
        <tool-tip style="pointer-events: none;" id="tooltip-2" for="contribution-day-component-3-50" popover="manual" data-direction="n" data-type="label" data-view-component="true" class="sr-only position-absolute">1,204 contributions on November 11th.</tool-tip>
        """;

    [Fact]
    public void Calendar_days_are_sorted_with_levels_and_counts()
    {
        var days = GitHubActivityService.ParseCalendar(Calendar);

        Assert.Equal(
            [new ContributionDay(new DateOnly(2026, 11, 10), 0, 0), new ContributionDay(new DateOnly(2026, 11, 11), 2, 1204)],
            days);
    }

    [Theory]
    [InlineData("""{"Time":"2026-11-20T10:00:00+00:00","Kind":"view","Tar""")]
    [InlineData("")]
    public void Broken_visit_line_is_skipped(string line) => Assert.Null(VisitLog.Parse(line));
}
