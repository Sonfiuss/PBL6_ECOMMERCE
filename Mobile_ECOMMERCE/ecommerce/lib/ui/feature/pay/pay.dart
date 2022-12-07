import 'package:ecommerce/ui/base/base_page.dart';
import 'package:ecommerce/ui/feature/pay/bloc/pay_presenter.dart';
import 'package:ecommerce/ui/feature/pay/bloc/pay_state.dart';

import 'package:ecommerce/ui/resources/app_colors.dart';
import 'package:ecommerce/ui/widget/button_common.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

import '../../../data/model/cart.dart';
import '../../../injection/injector.dart';
import 'components/body.dart';

class Pay extends BasePage {
  const Pay({Key? key, required this.listCart}) : super(key: key);
  final List<CartModel> listCart;

  @override
  State<Pay> createState() => _PayState();
}

class _PayState extends State<Pay> {
  @override
  void initState() {
    payPresenter.init(widget.listCart);
    super.initState();
  }

  final payPresenter = injector.get<PayPresenter>();
  @override
  Widget build(BuildContext context) {
    return BlocBuilder<PayPresenter, PayState>(
      bloc: payPresenter,
      builder: (context, state) => Scaffold(
        backgroundColor: AppColors.primary,
        appBar: AppBar(
          centerTitle: true,
          backgroundColor: AppColors.white,
          elevation: 0,
          leading: Builder(
            builder: (BuildContext context) {
              return IconButton(
                icon: const Icon(
                  Icons.arrow_back_ios_sharp,
                  color: AppColors.black,
                ),
                onPressed: () {
                  Scaffold.of(context).openDrawer();
                },
                tooltip: MaterialLocalizations.of(context).openAppDrawerTooltip,
              );
            },
          ),
          title: const Text(
            'Thanh Toán',
            style: TextStyle(
              fontSize: 14,
              fontWeight: FontWeight.w700,
              color: Colors.black,
            ),
          ),
        ),
        body: Body(payPresenter: payPresenter,),
        bottomNavigationBar: Container(
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
                  )
                ],
              ),
              ButtonCommon(
                txt: 'Đặt Hàng',
                width: MediaQuery.of(context).size.width,
                onTap: () {},
                colorButton: AppColors.red,
                colorText: AppColors.white,
              )
            ],
          ),
        ),
      ),
    );
  }
}
