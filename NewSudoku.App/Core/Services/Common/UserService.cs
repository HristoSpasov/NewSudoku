namespace NewSudoku.App.Core.Services.Common
{
    using NewSudoku.App.Interfaces.Repository;
    using NewSudoku.Entities;
    using NewSudoku.Services.Interfaces;

    public class UserService : IUserService
    {
        private readonly IInterfaceService interfaceService;
        private readonly IReaderService reader;
        private readonly IWriterService writer;
        private readonly IUserSessionService userSessionService;
        private readonly IPlayerRepository playerRepository;

        public UserService(IInterfaceService interfaceService, IReaderService reader, IWriterService writer, IUserSessionService userSessionService, IPlayerRepository playerRepository)
        {
            this.interfaceService = interfaceService;
            this.reader = reader;
            this.writer = writer;
            this.userSessionService = userSessionService;
            this.playerRepository = playerRepository;
        }

        public string Login()
        {
            string username = this.readUsername();

            Player newPlayer = this.playerRepository.Get(username);

            if (newPlayer == null)
            {
                newPlayer = new Player()
                {
                    Id = username
                };

                this.playerRepository.Insert(newPlayer); // If not registered => auto register player on login
            }

            this.userSessionService.Login(newPlayer);

            return username;
        }

        public void Logout()
        {
            this.userSessionService.Logout();
        }

        private string readUsername()
        {
            string name = default(string);

            while (true)
            {
                this.interfaceService.Clear();

                this.writer.Write("Enter user name: ");
                name = this.reader.ReadLine();

                if (name.Length < 3)
                {
                    this.writer.WriteLine("Username must be at least 3 characters! Press any key to try again!");
                    this.reader.ReadKeyboardInput();
                }
                else
                {
                    break;
                }
            }

            return name;
        }
    }
}