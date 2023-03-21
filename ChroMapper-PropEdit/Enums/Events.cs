namespace ChroMapper_PropEdit.Enums {

public static class Events {
	public static readonly Map<int> LightValues = new Map<int> {
		{0, "Off"},
		{1, "RightOn"},
		{2, "RightFlash"},
		{3, "RightFade"},
		{4, "RightTrans"},
		{5, "LeftOn"},
		{6, "LeftFlash"},
		{7, "LeftFade"},
		{8, "LeftTrans"},
		{9, "WhiteOn"},
		{10, "WhiteFlash"},
		{11, "WhiteFade"},
		{12, "WhiteTrans"},
	};
	
	public static readonly Map<int> LaserDirection = new Map<int> {
		{0, "Outward"},
		{1, "Inward"},
	};
	
	public static readonly Map<int> RingDirection = new Map<int> {
		{0, "Counter-Clockwise"},
		{1, "Clockwise"},
	};
	
	public static readonly Map Easings = new Map {
		{"easeLinear", "Linear"},
		{"easeInQuad", "Quadratic In"},
		{"easeOutQuad", "Quadratic Out"},
		{"easeInOutQuad", "Quadratic In/Out"},
		{"easeInCubic", "Cubic In"},
		{"easeOutCubic", "Cubic Out"},
		{"easeInOutCubic", "Cubic In/Out"},
		{"easeInQuart", "Quartic In"},
		{"easeOutQuart", "Quartic Out"},
		{"easeInOutQuart", "Quartic In/Out"},
		{"easeInQuint", "Quintic In"},
		{"easeOutQuint", "Quintic Out"},
		{"easeInOutQuint", "Quintic In/Out"},
		{"easeInSine", "Sine In"},
		{"easeOutSine", "Sine Out"},
		{"easeInOutSine", "Sine In/Out"},
		{"easeInExpo", "Exponential In"},
		{"easeOutExpo", "Exponential Out"},
		{"easeInOutExpo", "Exponential In/Out"},
		{"easeInCirc", "Circular In"},
		{"easeOutCirc", "Circular Out"},
		{"easeInOutCirc", "Circular In/Out"},
		{"easeInBack", "Back In"},
		{"easeOutBack", "Back Out"},
		{"easeInOutBack", "Back In/Out"},
		{"easeInElastic", "Elastic In"},
		{"easeOutElastic", "Elastic Out"},
		{"easeInOutElastic", "Elastic In/Out"},
		{"easeInBounce", "Bounce In"},
		{"easeOutBounce", "Bounce Out"},
		{"easeInOutBounce", "Bounce In/Out"}
	};
	
	public static readonly Map LerpTypes = new Map {
		{"RGB", "RGB"},
		{"HSV", "HSV"}
	};
}

}
