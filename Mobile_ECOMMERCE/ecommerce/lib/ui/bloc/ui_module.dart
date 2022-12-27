import '../../../../injection/injector.dart';
import 'ui_presenter.dart';

class UiModule {
  static Future<void> inject() async {
    injector.registerLazySingleton<UiPresenter>(
        () => UiPresenter());
  }
}
