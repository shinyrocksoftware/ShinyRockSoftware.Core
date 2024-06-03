using App.Background;
using Lab.App.Background;

var program = new DefaultBackgroundProgram();
program.RunDefaultPeriodic<PeriodicBackgroundService>(["Lab"], null, args);