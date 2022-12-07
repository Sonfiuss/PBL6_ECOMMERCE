import 'package:ecommerce/data/model/cart.dart';

import 'package:freezed_annotation/freezed_annotation.dart';

part 'cart_state.freezed.dart';
enum CartStatus {
  init,
  inProgress,
  success
}

@Freezed(makeCollectionsUnmodifiable: false)
class CartState with _$CartState {
  factory CartState({
    required CartStatus cartStatus,
    required int allPrice,
    required List<CartModel> cart,
    required List<IsCart> isCart
    ,
  }) = _CartState;

  const CartState._();

  factory CartState.initial() => CartState(allPrice: 0, cart: [], isCart: [],cartStatus:CartStatus.init );
}


class IsCart {
  IsCart({
     this.isStore,
     this.isProduct,
  });
  bool? isStore;
  List<bool>? isProduct;
}
