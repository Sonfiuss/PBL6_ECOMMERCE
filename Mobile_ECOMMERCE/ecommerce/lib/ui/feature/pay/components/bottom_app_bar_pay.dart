import 'package:ecommerce/ui/feature/pay/bloc/pay_presenter.dart';
import 'package:ecommerce/ui/feature/pay/bloc/pay_state.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../resources/app_colors.dart';
import '../../../widget/button_common.dart';

class BottomAppBarPay extends StatelessWidget {
  const BottomAppBarPay({
    Key? key,
    required this.payPresenter, required this.onTap,
  }) : super(key: key);
  final PayPresenter payPresenter;
  final Function() onTap;

  @override
  Widget build(BuildContext context) {
    return BlocBuilder<PayPresenter, PayState>(
      bloc: payPresenter,
      builder: (context, state) => Container(
        height: 100,
        padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 10),
        color: AppColors.white,
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                const Text('Tổng'),
                Text(
                  '${state.allPrice} VND',
                  style: const TextStyle(
                    color: AppColors.red,
                  ),
                ),
              ],
            ),
            ButtonCommon(
              txt: 'Đặt Hàng',
              width: MediaQuery.of(context).size.width,
              onTap: onTap,
              colorButton: AppColors.red,
              colorText: AppColors.white,
            ),
          ],
        ),
      ),
    );
  }
}
