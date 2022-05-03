using M133.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace M133.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ImportStudySetModel : PageModel
{
    private readonly QuizletContext _quizletContext;

    public ImportStudySetModel(QuizletContext quizletContext)
    {
        _quizletContext = quizletContext;
    }

    [BindProperty] public string Title { get; set; } = null!;

    [BindProperty] public string TextArea { get; set; } = null!;

    public StudySet? ImportStudySet()
    {
        var sep = "\t";
        var studySet = new StudySet(Title, _quizletContext.Users.First());
        var listStrLineElements = TextArea.Split(Environment.NewLine).ToList();
        foreach (var l in listStrLineElements)
        {
            var termandDefinition = l.Split(sep).ToList();
            if (termandDefinition.Count != 2) continue;
            var card = new Card(termandDefinition[0], termandDefinition[1]);
            studySet.Cards.Add(card);
        }

        return studySet;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    public IActionResult OnPostAsync()
    {
        var studySet = ImportStudySet();
        if (studySet == null)
            return Page();
        _quizletContext.StudySets.Add(studySet);
        _quizletContext.SaveChanges();
        return RedirectToPage("./Index");
    }
}