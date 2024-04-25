using System.Threading.Tasks;

namespace AppRouteSession03.PL.Services.EmailSender
{
	public interface IEmailSender
	{
		Task SendAsync(string form , string recipients , string subject , string body );
	}
}
