namespace m3uP.Services
{
	public interface IPlaylistProvider
	{
		string ReadChannels();
		string ReadEpg();
	}
}