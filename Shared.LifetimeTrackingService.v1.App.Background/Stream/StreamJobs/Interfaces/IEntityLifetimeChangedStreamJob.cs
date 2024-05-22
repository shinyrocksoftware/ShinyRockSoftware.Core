using Core.Model.Interface;
using Core.Stream.Interface;

namespace Shared.LifetimeTrackingService.v1.App.Background.Stream.StreamJobs.Interfaces;

public interface IEntityLifetimeChangedStreamJob : IStreamJob, IAutoInjection;