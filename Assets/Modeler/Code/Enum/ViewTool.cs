
namespace Modeler
{
	/**
	 * Describes different camera manipulation types.
	 */
	public enum ViewTool
	{
		None,	// Camera is not in control of anything
		Orbit,	// Camera is spherically rotating around target
		Pan,	// Camera is moving right or left
		Dolly,	// Camera is moving forward or backwards
		Look 	// Camera is looking and possibly flying
	}
}
