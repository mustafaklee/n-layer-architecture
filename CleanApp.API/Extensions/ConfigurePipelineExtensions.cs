namespace CleanApp.API.Extensions
{
    public static class ConfigurePipelineExtensions
    {
        public static IApplicationBuilder ConfigurePipelineExt(this WebApplication app)
        {

            app.UseExceptionHandler(x => { });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerGenExt();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            return app;
        }

    }
}
