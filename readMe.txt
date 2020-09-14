when first booting into the raspbery pi and trying to run the dotnet command you have to first to this 

export DOTNET_ROOT=$HOME/dotnet-arm32
export PATH=$PATH:$HOME/dotnet-arm32

./ngrok http https://localhost:5001 -host-header="localhost:5001"

simple REST api using the MVC .net core framework, mainly to learn c#. enter the name of the pokemon and rest api returns their stats, moves and the moves tier, type.
