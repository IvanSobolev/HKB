namespace HKB.DataClass;

public class Upgrades
{
    public TwoFactorAuthentication two_factor_authentication { get; set; }
    public AnonymousTransactionsBan anonymous_transactions_ban { get; set; }
    public LicenceJapan licence_japan { get; set; }
    public LicenceVietnam licence_vietnam { get; set; }
    public LicenceTurkey licence_turkey { get; set; }
    public TwoChairs two_chairs { get; set; }
    public ShortSqueeze short_squeeze { get; set; }
    public NotcoinListing notcoin_listing { get; set; }
    public Ceo21m ceo_21m { get; set; }
    public PremarketLaunch premarket_launch { get; set; }
}