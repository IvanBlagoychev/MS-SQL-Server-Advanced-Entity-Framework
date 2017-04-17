namespace PhotoShare.Client.Core
{
    using Commands;
    using System;
    using System.Linq;
    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string result = string.Empty;
            string command = commandParameters[0];
            //try
            //{   
                string[] data = commandParameters.Skip(1).ToArray();
                
                switch (command)
                {
                    case "RegisterUser":
                        RegisterUserCommand register = new RegisterUserCommand();
                        result = register.Execute(data);
                        break;
                    case "Exit":
                        ExitCommand exit = new ExitCommand();
                        result = exit.Execute();
                        break;
                    case "AddTown":
                        AddTownCommand addTown = new AddTownCommand();
                        result = addTown.Execute(data);
                        break;
                    case "ModifyUser":
                        ModifyUserCommand modify = new ModifyUserCommand();
                        result = modify.Execute(data);
                        break;
                    case "DeleteUser":
                        DeleteUser delete = new DeleteUser();
                        result = delete.Execute(data);
                        break;
                    case "AddTag":
                        AddTagCommand addtag = new AddTagCommand();
                        result = addtag.Execute(data);
                        break;
                    case "CreateAlbum":
                        CreateAlbumCommand addAlbum = new CreateAlbumCommand();
                        result = addAlbum.Execute(data);
                        break;
                    case "AddTagTo":
                        AddTagToCommand addTagTo = new AddTagToCommand();
                        result = addTagTo.Execute(data);
                        break;
                    case "MakeFriends":
                        MakeFriendsCommand makeFriend = new MakeFriendsCommand();
                        result = makeFriend.Execute(data);
                        break;
                    case "ListFriends":
                        PrintFriendsListCommand listFriends = new PrintFriendsListCommand();
                        result = listFriends.Execute(data);
                        break;
                    case "ShareAlbum":
                        ShareAlbumCommand shareAlbum = new ShareAlbumCommand();
                        result = shareAlbum.Execute(data);
                        break;
                    case "UploadPicture":
                        UploadPictureCommand uploadPicture = new UploadPictureCommand();
                        result = uploadPicture.Execute(data);
                        break;
                    case "Login":
                        LoginCommand login = new LoginCommand();
                        result = login.Execute(data);
                        break;
                    case "Logout":
                        LogoutCommand logout = new LogoutCommand();
                        result = logout.Execute();
                        break;
                    default: throw new InvalidOperationException($"Command {command} not valid!");
                }
            //}
            //catch (Exception)
            //{

                //return $"Command {command} not valid!";
            //}

            return result;
        }
    }
}
