import 'package:ecommerce/ui/feature/cart/bloc/cart_presenter.dart';
import 'package:ecommerce/ui/feature/page_map/bloc/map_presenter.dart';

import '../../../../injection/injector.dart';
import 'pay_presenter.dart';

class PayModule {
  static Future<void> inject() async {
    injector.registerLazySingleton<PayPresenter>(
      () => PayPresenter(injector.get<CartPresenter>().state),
    );
  }
}