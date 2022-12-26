import 'package:ecommerce/ui/feature/cart/bloc/cart_presenter.dart';
import 'package:ecommerce/ui/feature/page_map/bloc/map_presenter.dart';

import '../../../../injection/injector.dart';
import 'sign_in_presenter.dart';

class SignInModule {
  static Future<void> inject() async {
    injector.registerLazySingleton<SignInPresenter>(
      () => SignInPresenter(),
    );
  }
}
