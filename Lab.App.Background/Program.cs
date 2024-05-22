using App.Background.Periodic;
using Lab.App.Background;

var program = new PeriodicBackgroundProgram();
program.Run<PeriodicBackgroundService>(["Lab"], null, args);