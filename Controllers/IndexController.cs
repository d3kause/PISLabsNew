using Microsoft.AspNetCore.Mvc;

namespace PISLabs.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IndexController
    {
        [HttpGet]
        [HttpGet("{id}")]
        public ContentResult Index()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                Content = "<H1 align = center>Some api information</H1>" +
                "<p> This application is the work of students of NSTU, group FBI-71 Igonin Valery and Asharafulina Julia.</p>" +
                "<p>The work was carried out for 6 - 7 semesters in the discipline \"Design of information systems.\"</p>" +
                "<hr />" +
                "<p>In the process of performing the work, the authors studied:" +
                "<ul>" +
                "<li>.net core;</li>" +
                "<li>Local git and GitHub;</li>" +
                "<li>NuGet;</li>" +
                "<li>TeamCity;</li>" +
                "<li>docker.</li>" +
                "</ul>" +
                "<p>They also learned how to use Dependency Injections, PUTTY and WinCSP applications, and some C # libraries such as GitVersion.</p>" +
                "<p>The work was performed under the guidance of the teacher of the NSTU Pavlov Pavel Sergeevich.</p>" +
                "<hr />" +
                "<p>Feedback data for communication:" +
                "<ul><li>Mail: dekause@mail.ru</li>" +
                "<li>Telegram: @dekause</li></ul></p>" +
                "<hr />" +
                "<p align = \"center\">Thanks for the knowledge provided!</p>"


            };
        }
    }
}
