namespace NewSudoku.App.Core.Commands.ReportCommands
{
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using NewSudoku.App.Interfaces.Core;
    using NewSudoku.App.Interfaces.Repository;
    using NewSudoku.Services.Interfaces;

    public class ByUsernameCommand : ICommand
    {
        private readonly IPlayerRepository playerRepository;
        private readonly IFileService fileService;

        public ByUsernameCommand(IPlayerRepository playerRepository, IFileService fileService)
        {
            this.playerRepository = playerRepository;
            this.fileService = fileService;
        }

        public void Execute(params string[] args)
        {
            StringBuilder output = new StringBuilder();

            this.playerRepository
                .GetAll()
                .OrderBy(p => p.Id)
                .ToList()
                .ForEach(e => output.AppendLine(e.ToString()));

            this.fileService.WriteFile("./report.txt", output.ToString());

            Process.Start("notepad.exe", "./report.txt");
        }
    }
}
