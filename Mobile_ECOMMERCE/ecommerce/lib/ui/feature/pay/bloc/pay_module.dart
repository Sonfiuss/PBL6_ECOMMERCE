import 'package:ecommerce/ui/feature/cart/bloc/cart_presenter.dart';
import 'package:ecommerce/ui/feature/page_map/bloc/map_presenter.dart';

import '../../../../injection/injector.dart';
import '../../../bloc/ui_presenter.dart';
import 'pay_presenter.dart';

class PayModule {
  static Future<void> inject() async {
    injector.registerLazySingleton<PayPresenter>(
      () => PayPresenter(
        cartPresenter: injector.get<CartPresenter>(),
        uiState: injector.get<UiPresenter>().state,
      ),
    );
  }
}
