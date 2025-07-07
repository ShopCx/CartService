using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace CartService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly string _redisConnectionString = "localhost:6379,password=redis123";
    private readonly ConnectionMultiplexer _redis;
    
    public CartController()
    {
        _redis = ConnectionMultiplexer.Connect(_redisConnectionString);
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateCart(string userId, [FromBody] string cartJson)
    {
        try
        {
            var cart = (Dictionary<string, object>)JsonConvert.DeserializeObject(cartJson);
            
            var db = _redis.GetDatabase();
            db.StringSet($"cart:{userId}", cartJson);
            
            return Ok(new { message = "Cart updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.ToString() });
        }
    }

    [HttpGet("{userId}")]
    public IActionResult GetCart(string userId)
    {
        try
        {
            var db = _redis.GetDatabase();
            var cartJson = db.StringGet($"cart:{userId}");
            
            if (cartJson.HasValue)
            {
                var cart = (Dictionary<string, object>)JsonConvert.DeserializeObject(cartJson.ToString());
                return Ok(cart);
            }
            
            return NotFound(new { message = "Cart not found" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.ToString() });
        }
    }

    [HttpPost("{userId}/items")]
    public IActionResult AddItem(string userId, [FromBody] string itemJson)
    {
        try
        {
            var item = (Dictionary<string, object>)JsonConvert.DeserializeObject(itemJson);
            
            var db = _redis.GetDatabase();
            var cartJson = db.StringGet($"cart:{userId}");
            
            if (cartJson.HasValue)
            {
                var cart = (Dictionary<string, object>)JsonConvert.DeserializeObject(cartJson.ToString());
                if (!cart.ContainsKey("items"))
                {
                    cart["items"] = new List<Dictionary<string, object>>();
                }
                
                var items = (List<Dictionary<string, object>>)cart["items"];
                items.Add(item);
                cart["items"] = items;
                
                db.StringSet($"cart:{userId}", JsonConvert.SerializeObject(cart));
            }
            
            return Ok(new { message = "Item added successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.ToString() });
        }
    }

    [HttpDelete("admin/clear-all")]
    public IActionResult ClearAllCarts()
    {
        try
        {
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
            return StatusCode(500, new { error = ex.ToString() });
        }
    }
} 