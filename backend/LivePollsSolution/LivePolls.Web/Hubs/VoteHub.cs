//using Chat.Models;
using Microsoft.AspNetCore.SignalR;
//using StackExchange.Redis;
using System.Text.Json;


namespace LivePolls.Web.Hubs
{
    public class VoteHub : Hub
    {
       
        //private static List<Item> VoteItems = new List<Item>();
        //private static VoteRContext db = new VoteRContext();

        private static Dictionary<string, int> _pollData = new() { { "OptionA", 0 }, { "OptionB", 0 } };

        //public void CreateVote(string UserName, int UserId, string[] question) 
        //{
        //    var votes = AddVote(id);
        //    Clients.All.updateVoteResults(id, votes);
        //}

        // отдать голос
        public async Task CastVote(string option)
        {
            if (_pollData.ContainsKey(option))
            {
                _pollData[option]++;
                // Broadcast the updated vote count to everyone
                await Clients.All.SendAsync("UpdateResults", option, _pollData[option]);
            }
        }
        //public void Vote(int id)
        //{
        //    var votes = AddVote(id);
        //    Clients.All.updateVoteResults(id, votes);
        //}

        //private static Item AddVote(int id)
        //{
        //    var voteItem = VoteItems.Find(v => v.Id == id);
        //    if (voteItem != null)
        //    {
        //        voteItem.Votes++;
        //        return voteItem;
        //    }
        //    else
        //    {
        //        var item = db.Items.Find(id);
        //        item.Votes++;
        //        VoteItems.Add(item);
        //        return item;
        //    }
        //}

        //public override Task OnConnected()
        //{
        //    Clients.Caller.joinVoting(VoteItems.ToList());
        //    return base.OnConnected();
        //}

        //public override Task OnConnectedAsync()
        //{
        //    Clients.Caller.joinVoting(VoteItems.ToList());
        //    return base.OnConnectedAsync();
        //}
    }
}
