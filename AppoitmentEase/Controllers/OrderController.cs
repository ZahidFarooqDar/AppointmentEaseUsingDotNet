using AutoMapper;
using EcommereAPI.Data;
using EcommereAPI.DomainModels;
using EcommereAPI.Helpers;
using EcommereAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace EcommereAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly ProjectEcommerceContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiKey;
        private readonly HttpClient _httpClient;

        public OrderController(IHttpClientFactory httpClientFactory, ProjectEcommerceContext projectEcommerceContext, IMapper mapper, HttpClient httpClient)
        {
            _httpClientFactory = httpClientFactory;
            _context = projectEcommerceContext;
            _mapper = mapper;
            _httpClient = httpClient;
            this.apiKey = apiKey;
            this._httpClient.BaseAddress = new Uri("https://api.easyship.com/v2/");
        }
      /*  [HttpPost("generate-rates")]
        public async Task<IActionResult> GenerateShippingRates([FromBody] Shipment shipment)
        {
            try
            {
                var shippingRates = await GetShippingRates(shipment);
                return Ok(shippingRates);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    public async Task<List<ShippingRate>> GetShippingRates(Shipment shipment)
    {
        var json = JsonSerializer.Serialize(shipment);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, "shipping_rates");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        request.Content = content;

        var response = await httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<ShippingRate>>(jsonResponse);
        }
        else
        {
            throw new Exception($"Failed to get shipping rates: {response.StatusCode}");
        }
    }*/

    [HttpPost("easyShip")]
        public async Task<IActionResult> CreateEasyShipProduct([FromBody] Product product)
        {
            var baseUrl = "https://api.easyship.com/2023-01";
            var endpoint = "/products";
            var apiKey = "sand_L8sAYQGFTaLADcN/gk3JBLvWujzEZXxfeqG7rJlGPsg="; 

            var client = new RestClient(baseUrl);
            var request = new RestRequest(endpoint, Method.Post);
            request.AddHeader("accept", "application/json");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", $"Bearer {apiKey}");
            request.AddJsonBody(product);

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            else
            {
                return BadRequest(response.Content);
            }
        }

        [HttpPost("track")]
        public async Task<IActionResult> TrackOrder([FromBody] Ship24TrackingRequest trackingRequest)
        {

            try
            {
                // Use HttpClient to make a POST request to the Ship24 API
                var client = _httpClientFactory.CreateClient();
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                var response = await client.PostAsJsonAsync("https://api.ship24.com/public/v1/trackers", trackingRequest);

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the Ship24 API response
                    //var ship24Response = await response.Content.ReadAsAsync<Ship24TrackingResponse>();
                    var ship24Response = await response.Content.ReadAsStringAsync();
                    // Handle the response data as needed
                    return Ok(ship24Response);
                }
                else
                {
                    // Handle the error response from the Ship24 API
                    return BadRequest("Error from Ship24 API: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the API request
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("trackers")]
        public async Task<IActionResult> ListTrackers()
        {
            try
            {
                // Create a new HttpClient and set the Authorization header
                var client = _httpClientFactory.CreateClient();
                // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                // Make a GET request to the Ship24 API to list existing trackers
                var response = await client.GetAsync("https://api.ship24.com/public/v1/trackers");

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the response manually into a list of TrackerModel
                    var trackers = JsonConvert.DeserializeObject<List<TrackerModel>>(responseBody);

                    // Handle the list of trackers as needed
                    return Ok(trackers);
                }
                else
                {
                    // Handle the error response from the Ship24 API
                    return BadRequest("Error from Ship24 API: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the API request
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
       // [HttpPost("trackers/track")]
        /*public async Task<IActionResult> CreateAndTrackTracker([FromHeader] string apiKey, [FromBody] Ship24TrackingRequest trackingRequest)
        {
            try
            {
                // Create a new HttpClient and set the Authorization header
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                // Make a POST request to the Ship24 API to create and track a tracker
                var response = await client.PostAsJsonAsync("https://api.ship24.com/public/v1/trackers/track", trackingRequest);

                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var responseBody = await response.Content.ReadAsStringAsync();

                    // Deserialize the response manually into a TrackerResultModel
                    var trackerResult = JsonConvert.DeserializeObject<TrackerResultModel>(responseBody);

                    // Handle the tracker result as needed
                    return Ok(trackerResult);
                }
                else
                {
                    // Handle the error response from the Ship24 API
                    return BadRequest("Error from Ship24 API: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the API request
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }*/




        // POST api/orders
        /*[HttpPost]
        public async Task<IActionResult> CreateOrderWithProducts([FromBody] OrderWithProductsRequestDTO orderRequest)
        {
            try
            {
                // Create the main order entry
                var order = new OrderDM
                {
                    BuyerId = orderRequest.BuyerId,
                    // Other order properties
                };

                // Create a list to hold order details
                var orderDetailsList = new List<OrderDetailDM>();

                foreach (var productRequest in orderRequest.Products)
                {
                    // Create an order detail for each product
                    var orderDetail = new OrderDetailDM
                    {
                        ProductId = productRequest.ProductId,
                        Quantity = productRequest.Quantity,
                        // Other order detail properties
                    };

                    // Generate a TrackingId based on shipment address (you may need to implement this logic)
                    string trackingId = GenerateTrackingId(productRequest.ShipmentAddress);
                    orderDetail.OrderId = trackingId;

                    orderDetailsList.Add(orderDetail);
                }

                order.OrderDetails = orderDetailsList;

                // Save the order and its details to the database
                await _orderService.CreateOrder(order);

                return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating order: {ex.Message}");
            }
        }

        [HttpGet("{orderId}/track")]
        public async Task<IActionResult> TrackOrder(int orderId)
        {
            try
            {
                // Retrieve the order from the database based on the orderId
                var order = await _orderService.GetOrderById(orderId);

                if (order == null)
                {
                    return NotFound($"Order with ID: {orderId} not found.");
                }

                // Implement the logic to track the order using FedEx or your chosen shipping carrier's API.
                // You can iterate through the order's OrderDetails and use each TrackingId for tracking.

                // Example:
                var trackingInfoList = new List<TrackingInfo>();

                foreach (var orderDetail in order.OrderDetails)
                {
                    string trackingId = orderDetail.TrackingId;
                    // Call FedEx API or your carrier's API to get tracking information for trackingId
                    // Add tracking information to the trackingInfoList
                }

                return Ok(trackingInfoList);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error tracking order: {ex.Message}");
            }
        }
*/



    }
}
