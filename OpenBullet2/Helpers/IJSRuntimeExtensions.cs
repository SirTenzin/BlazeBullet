﻿using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace OpenBullet2.Helpers
{
    public static class IJSRuntimeExtensions
    {
        public async static Task Log(this IJSRuntime js, string message)
            => await js.InvokeVoidAsync("console.log", message);

        public async static Task Alert(this IJSRuntime js, string message)
            => await js.InvokeVoidAsync("Swal.fire", message);

        public async static Task AlertSuccess(this IJSRuntime js, string title, string message)
            => await js.InvokeVoidAsync("Swal.fire", title, message, "success");

        public async static Task AlertError(this IJSRuntime js, string title, string message)
            => await js.InvokeVoidAsync("Swal.fire", title, message, "error");

        public async static Task<bool> Confirm(this IJSRuntime js, string question, string message)
        {
            var options = new
            {
                title = question,
                text = message,
                icon = "warning",
                showCancelButton = true,
                confirmButtonColor = "#3085d6",
                cancelButtonColor = "#d33",
                confirmButtonText = "OK"
            };

            var result = await js.InvokeAsync<object>("Swal.fire", options);
            var obj = JObject.Parse(result.ToString());

            if (obj.TryGetValue("value", out JToken value))
                return value.ToObject<bool>();

            return false;
        }
    }
}