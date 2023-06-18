namespace BluToolbox
{
  public static class AwaitConstants
  {
    public static TxongaWaitForSeconds Segurada                   => new TxongaWaitForSeconds(0.5f);
    public static TxongaWaitForSeconds Gordurinha                 => new TxongaWaitForSeconds(0.1f);
    public static TxongaWaitForSeconds WaitQuarterSecond          => new TxongaWaitForSeconds(0.25f);
    public static TxongaWaitForSeconds WaitHalfSecond             => new TxongaWaitForSeconds(0.5f);
    public static TxongaWaitForSeconds WaitOneSecond              => new TxongaWaitForSeconds(1.0f);
    public static TxongaWaitForSeconds WaitTwoSeconds             => new TxongaWaitForSeconds(2.0f);
    public static TxongaWaitForSeconds WaitThreeSeconds           => new TxongaWaitForSeconds(3.0f);
    public static TxongaWaitForSeconds WaitFiveSeconds            => new TxongaWaitForSeconds(5.0f);
    public static TxongaWaitForSeconds WithSeconds(float seconds) => new TxongaWaitForSeconds(seconds);
  }
}