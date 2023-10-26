using Hart_Check_Official.DTO;
using Hart_Check_Official.Interface;
using Hart_Check_Official.Models;
using Hart_Check_Official.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Hart_Check_Official.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly HttpClient _httpClient;

        public PaymentController(IUserRepository userRepository, IHttpClientFactory httpClientFactory)
        {
            _userRepository = userRepository;
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("/generate-invoice")]
        public async Task<IActionResult> GenerateInvoice([FromBody] PaymentDto userInvoice)
        {
            string url = "https://pg-sandbox.paymaya.com/invoice/v2/invoices";

            var request = new RequestModel
            {
                invoiceNumber = "INV0001",
                type = "SINGLE",
                totalAmount = new TotalAmountModel
                {
                    value = 300,
                    currency = "PHP"
                },
                items = new List<ItemModel>
                {
                    new ItemModel
                    {
                        name = "HartCheck Subscription",
                        quantity = "1",
                        totalAmount = new TotalAmountModel
                        {
                            value = 300,
                            currency = "PHP"
                        }
                    }
                },
                redirectUrl = new RedirectModel
                {
                    success = "https://www.merchantsite.com/success",//change to payment success go back to mobile app
                    failure = "https://www.merchantsite.com/failure",//payment failed html
                    cancel = "https://www.merchantsite.com/cancel"//payment has been cancelled html
                },
                requestReferenceNumber = "1551191039",
                metadata = new { }
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes("sk-X8qolYjy62kIzEbr0QRK1h4b4KDVHaNcwMYk39jInSl")));

            var response = await _httpClient.PostAsync(url, content);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var user = _userRepository.GetUsersEmail(userInvoice.email);
                var invoiceUrl = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(responseJson).invoiceUrl;
                //Email code here
                var smtpClient = new SmtpClient("smtp.gmail.com") // Replace with your SMTP server
                {
                    Port = 587, // Replace with your SMTP server's port
                    Credentials = new NetworkCredential("testing072301@gmail.com", "dsmnmkocsoyqfvhz"), // Replace with your SMTP server's username and password
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(user.email), // Replace with the sender's email
                    Subject = "Pending Payment - HartCheck",
                    Body = $"Click here to pay for the subscription: {invoiceUrl}"
                };

                mailMessage.To.Add(userInvoice.email);
                smtpClient.Send(mailMessage);

                return Ok(new { invoiceUrl });
            }
            else
            {
                return BadRequest("Error: " + responseJson);
            }
        }
    }
}
