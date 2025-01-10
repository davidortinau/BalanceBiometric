using CommunityToolkit.Mvvm.Input;
using BalanceBiometric.Models;

namespace BalanceBiometric.PageModels;

public interface IProjectTaskPageModel
{
	IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
	bool IsBusy { get; }
}