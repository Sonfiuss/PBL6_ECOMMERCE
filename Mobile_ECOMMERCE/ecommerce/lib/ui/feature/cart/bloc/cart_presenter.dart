import 'dart:ffi';

import 'package:ecommerce/data/model/cart.dart';
import 'package:ecommerce/ui/feature/cart/bloc/cart_state.dart';
import 'package:ecommerce/ui/feature/cart/components/cart_counter.dart';
import 'package:ecommerce/ui/feature/detail/bloc/detail_state.dart';
import 'package:flutter/material.dart';

import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:freezed_annotation/freezed_annotation.dart';

class CartPresenter extends Cubit<CartState> {
  CartPresenter(
    this._detailState, {
    @visibleForTesting CartState? state,
  }) : super(state ?? CartState.initial());

  final DetailState _detailState;

  Future init() async {
    
    emit(state.copyWith(cart: _detailState.cart, cartStatus: CartStatus.init));
    var listIsCart = <IsCart>[];
    var isProduct = <bool>[];

    for (var i = 0; i < state.cart.length; i++) {
      for (var j = 0; j < state.cart[i].itemProduct.length; j++) {
        isProduct.add(true);
      }
      listIsCart.add(IsCart(
        isStore: true,
        isProduct: isProduct,
      ));
    }

    emit(
      state.copyWith(isCart: listIsCart, cartStatus: CartStatus.inProgress),
    );
    var sum = allPrice();
    emit(
      state.copyWith(allPrice: sum, cartStatus: CartStatus.success),
    );
  }

  void onAddAmount(int idStore, idItem) {
    var cartModel = <CartModel>[];
    state.cart[idStore].itemProduct[idItem].amount += 1;
    cartModel.addAll(state.cart);
    emit(
      state.copyWith(
        cart: cartModel,
        allPrice: allPrice(),
      ),
    );
  }

  void onReduceAmount(int idStore, idItem) {
    var cartModel = <CartModel>[];
    state.cart[idStore].itemProduct[idItem].amount -= 1;
    if (state.cart[idStore].itemProduct[idItem].amount < 0) {
      state.cart[idStore].itemProduct[idItem].amount = 0;
    }
    cartModel.addAll(state.cart);
    emit(
      state.copyWith(
        cart: cartModel,
        allPrice: allPrice(),
      ),
    );
  }

  void isCheck(int idStore, idItem) {
    emit(
      state.copyWith(cartStatus: CartStatus.inProgress),
    );
    var listIsCart = <IsCart>[];
    listIsCart.addAll(state.isCart);
    listIsCart[idStore].isProduct![idItem] =
        !state.isCart[idStore].isProduct![idItem];

    emit(
      state.copyWith(
          isCart: [...listIsCart],
          cartStatus: CartStatus.success,
          allPrice: allPrice()),
    );
    print(state.isCart[idStore].isProduct![idItem]);
  }

  void onDismissed({required int idStore, required idItem}) {
    final List<CartModel> cartModel = [];

    state.cart[idStore].itemProduct
        .remove(state.cart[idStore].itemProduct[idItem]);
    cartModel.addAll(state.cart);

    emit(
      state.copyWith(
        cart: cartModel,
        allPrice: allPrice(),
      ),
    );
  }

  void chooseStore(int idStore) {
    var listIs = <IsCart>[];
    emit(state.copyWith(cartStatus: CartStatus.inProgress));
    listIs.addAll(state.isCart);
    listIs[idStore].isStore = !state.isCart[idStore].isStore!;
    for (var i = 0; i < state.isCart[idStore].isProduct!.length; i++) {
      state.isCart[idStore].isProduct![i] = listIs[idStore].isStore!;
    }

    emit(state.copyWith(
        isCart: listIs, allPrice: allPrice(), cartStatus: CartStatus.success));
  }

  int allPrice() {
    int sum = 0;

    for (var i = 0; i < state.cart.length; i++) {
      for (var j = 0; j < state.cart[i].itemProduct.length; j++) {
        if (state.isCart[i].isProduct![j] == true) {
          sum += (state.cart[i].itemProduct[j].price *
              state.cart[i].itemProduct[j].amount);
        }
      }
    }
    return sum;
  }

  List<IsCart> initIsCheck() {
    var listIsCart = <IsCart>[];
    var isProduct = <bool>[];

    for (var i = 0; i < state.cart.length; i++) {
      for (var j = 0; j < state.cart[i].itemProduct.length; j++) {
        isProduct.add(true);
      }
      listIsCart.add(IsCart(
        isStore: true,
        isProduct: isProduct,
      ));
    }
    return listIsCart;
  }
}
