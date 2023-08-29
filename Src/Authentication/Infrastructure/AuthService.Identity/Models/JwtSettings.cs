namespace AuthService.Identity.Models {
    public class JwtSettings {
        public string Secret { get; set; } = null!;
        public TimeSpan TokenLifeTime { get; set; }
    }
}
