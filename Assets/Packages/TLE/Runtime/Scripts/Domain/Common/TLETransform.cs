namespace TLE.Runtime.Domain
{
    public record TLETransform(
        string reference_frame,
        TLEVector transform,
        TLEVector velocity
    );
}