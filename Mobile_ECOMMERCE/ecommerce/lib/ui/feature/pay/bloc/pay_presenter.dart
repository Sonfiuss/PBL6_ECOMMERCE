import 'package:ecommerce/data/model/cart.dart';
import 'package:ecommerce/ui/feature/cart/bloc/cart_state.dart';

import 'package:flutter/material.dart';

import 'package:flutter_bloc/flutter_bloc.dart';
import '../../../../utilities/helpers/validator_helper/validator_helper.dart';

import 'pay_state.dart';

class PayPresenter extends Cubit<PayState> {
  PayPresenter(
    this.cartState, {
    @visibleForTesting PayState? state,
  }) : super(
          state ?? PayState.initial(),
        );
  final CartState cartState;

  void init(List<CartModel> cart) {
   
    // cartPay.add(cartState.cart[cartState.isCart.indexOf()]);
    emit(
      state.copyWith(
        cart: cart,
        address: "Đại Học Bách Khoa",
        payStatus: PayStatus.inProgress,
      ),
    );

    var pricePay = _sumPrice();
    emit(
      state.copyWith(
        allPrice: ValidatorHelper().setupPrice(pricePay),
        pricePay: pricePay,
        payStatus: PayStatus.success,
      ),
    );
  }

  void getAddress(String address) {
    emit(state.copyWith(address: address));
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
