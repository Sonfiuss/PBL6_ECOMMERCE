import 'package:freezed_annotation/freezed_annotation.dart';

import '../../../../data/model/cart.dart';

part 'pay_state.freezed.dart';

enum PayStatus {
   init,
  inProgress,
  success
}
@freezed
class PayState with _$PayState {
  factory PayState({
    required int current,
    required PayStatus payStatus,
     required int allPrice,
    required List<CartModel> cart,
   
  }) = _PayState;

  const PayState._();

  factory PayState.initial() =>PayState(
        current:-1,
        payStatus: PayStatus.init,
        cart: [],
        allPrice:0
       
      );
}
