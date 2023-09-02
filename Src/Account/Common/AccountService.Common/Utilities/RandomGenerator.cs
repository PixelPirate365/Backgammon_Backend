namespace AccountService.Common.Utilities {
    public static class RandomGenerator {
        public static int GetRandomReward() {
            Random random = new Random();
            int randomValue = random.Next(200, 2001);
            return randomValue;
        }

    }
}
