SkillFunc <- function ( _success )
{
	SetupImageSize(210, 64);

	if (_success)
	{
		SetupProduction(2, 0, 4, 20);
		SetupNum(5, 0, 1, 1, 1);
		SetupTrigger(4, 13);
		SetupEnable(1, "bal", 0, 0, 0, 0);
		SetupEnable(2, "bal", 0, 0, 0, 0);
		SetupEnable(3, "bal", 0, 0, 0, 0);
	}
	else
	{
		SetupProduction(2, 0, 4, 20);
		SetupNum(4, 0, 1, 1, 1);
		SetupMotion(4, "04a");
		SetupTrigger(4, 13);
		SetupEnable(1, "bal", 0, 0, 0, 0);
		SetupEnable(2, "bal", 0, 0, 0, 0);
		SetupEnable(3, "bal", 0, 0, 0, 0);
	}

};
