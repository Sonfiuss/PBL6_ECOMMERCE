import 'package:flutter/material.dart';

import '../../../resources/app_colors.dart';

class AllPrice extends StatelessWidget {
  const AllPrice({
    Key? key,
    required this.sum,
  }) : super(key: key);
  final String sum;

  @override
  Widget build(BuildContext context) {
    return Container(
      color: AppColors.white,
      padding: const EdgeInsets.symmetric(
        horizontal: 20,
        vertical: 8,
      ),
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          const Text(
            'Tổng số tiền : ',
            style: TextStyle(
              fontSize: 11,
              fontWeight: FontWeight.w300,
            ),
          ),
          Text(
            '$sum VND',
            style: const TextStyle(
              color: AppColors.red,
            ),
          )
        ],
      ),
    );
  }
}
