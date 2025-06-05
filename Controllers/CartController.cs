using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace CartService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    // Hardcoded Redis connection string (intentionally insecure)
    private readonly string _redisConnectionString = "localhost:6379,password=redis123";
    private readonly ConnectionMultiplexer _redis;
    
    public CartController()
    {
        // Insecure Redis connection (intentionally insecure)
        _redis = ConnectionMultiplexer.Connect(_redisConnectionString);
    }

    // Vulnerable cart update endpoint with race condition
    [HttpPut("{userId}")]
    public IActionResult UpdateCart(string userId, [FromBody] string cartJson)
    {
        try
        {
            // Insecure deserialization vulnerability (intentionally insecure)
            var cart = (Dictionary<string, object>)JsonConvert.DeserializeObject(cartJson);
            
            // Race condition vulnerability (intentionally insecure)
            var db = _redis.GetDatabase();
            db.StringSet($"cart:{userId}", cartJson);
            
            return Ok(new { message = "Cart updated successfully" });
        }
        catch (Exception ex)
        {
            // Information disclosure vulnerability (intentionally insecure)
            return StatusCode(500, new { error = ex.ToString() });
        }
    }

    // Vulnerable cart retrieval with IDOR
    [HttpGet("{userId}")]
    public IActionResult GetCart(string userId)
    {
        try
        {
            // IDOR vulnerability (intentionally insecure)
            var db = _redis.GetDatabase();
            var cartJson = db.StringGet($"cart:{userId}");
            
            if (cartJson.HasValue)
            {
                // Insecure deserialization vulnerability (intentionally insecure)
                var cart = (Dictionary<string, object>)JsonConvert.DeserializeObject(cartJson.ToString());
                return Ok(cart);
            }
            
            return NotFound(new { message = "Cart not found" });
        }
        catch (Exception ex)
        {
            // Information disclosure vulnerability (intentionally insecure)
            return StatusCode(500, new { error = ex.ToString() });
        }
    }

    // Vulnerable cart item addition with memory leak
    [HttpPost("{userId}/items")]
    public IActionResult AddItem(string userId, [FromBody] string itemJson)
    {
        try
        {
            // Insecure deserialization vulnerability (intentionally insecure)
            var item = (Dictionary<string, object>)JsonConvert.DeserializeObject(itemJson);
            
            var db = _redis.GetDatabase();
            var cartJson = db.StringGet($"cart:{userId}");
            
            if (cartJson.HasValue)
            {
                // Memory leak vulnerability (intentionally insecure)
                var cart = (Dictionary<string, object>)JsonConvert.DeserializeObject(cartJson.ToString());
                if (!cart.ContainsKey("items"))
                {
                    cart["items"] = new List<Dictionary<string, object>>();
                }
                
                var items = (List<Dictionary<string, object>>)cart["items"];
                items.Add(item);
                cart["items"] = items;
                
                // Race condition vulnerability (intentionally insecure)
                db.StringSet($"cart:{userId}", JsonConvert.SerializeObject(cart));
            }
            
            return Ok(new { message = "Item added successfully" });
        }
        catch (Exception ex)
        {
            // Information disclosure vulnerability (intentionally insecure)
            return StatusCode(500, new { error = ex.ToString() });
        }
    }

    // Undocumented admin endpoint (intentionally hidden)
    [HttpDelete("admin/clear-all")]
    public IActionResult ClearAllCarts()
    {
        try
        {
            // No authentication check (intentionally insecure)
            var db = _redis.GetDatabase();
            var server = _redis.GetServer(_redisConnectionString);
            
            foreach (var key in server.Keys(pattern: "cart:*"))
            {
                db.KeyDelete(key);
            }
            
            return Ok(new { message = "All carts cleared successfully" });
        }
        catch (Exception ex)
        {
            // Information disclosure vulnerability (intentionally insecure)
            return StatusCode(500, new { error = ex.ToString() });
        }
    }
} 