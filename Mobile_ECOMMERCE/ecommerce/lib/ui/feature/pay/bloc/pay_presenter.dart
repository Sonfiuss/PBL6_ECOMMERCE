import 'package:ecommerce/data/model/cart.dart';
import 'package:ecommerce/ui/feature/cart/bloc/cart_state.dart';
import 'package:flutter/material.dart';

import 'package:flutter_bloc/flutter_bloc.dart';
import 'pay_state.dart';

class PayPresenter extends Cubit<PayState> {
  PayPresenter({
    @visibleForTesting PayState? state,
  }) : super(
          state ?? PayState.initial(),
        );
  void init(List<CartModel> cart) {
    emit(state.copyWith(
      cart: cart,
      payStatus: PayStatus.inProgress,
    ));
    var sum = _sumPrice();
    emit(
      state.copyWith(allPrice: sum, payStatus: PayStatus.success),
    );
  }

  void onTapIndex(int index) {
    emit(
      state.copyWith(current: index),
    );
  }

  int _sumPrice() {
    int sum = 0;

    for (var i = 0; i < state.cart.length; i++) {
      for (var j = 0; j < state.cart[i].itemProduct.length; j++) {
        sum += (state.cart[i].itemProduct[j].price *
            state.cart[i].itemProduct[j].amount);
      }
    }
    return sum;
  }
}
