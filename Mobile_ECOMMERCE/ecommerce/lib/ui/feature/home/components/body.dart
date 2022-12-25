import 'package:ecommerce/ui/feature/detail/detail.dart';
import 'package:ecommerce/ui/feature/home/components/search.dart';
import 'package:ecommerce/ui/feature/profile/profile.dart';
import 'package:flutter/material.dart';
import '../../../../data/model/product.dart';
import '../../../resources/app_colors.dart';
import '../../cart/cart.dart';
import 'genres.dart';
import 'item_product.dart';

class Body extends StatelessWidget {
  const Body({
    Key? key,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: SingleChildScrollView(
        child: SizedBox(
          child: Padding(
            padding: const EdgeInsets.symmetric(
              horizontal: 18,
              vertical: 30,
            ),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Row(
                  children: [
                    GestureDetector(
                      onTap: () => Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => const Profile(),
                        ),
                      ),
                      child: const CircleAvatar(
                        backgroundImage: NetworkImage(
                            'https://cdn.dribbble.com/users/3862254/avatars/normal/45a060c73c1034e79e583ec139846952.jpg?1664573660'),
                      ),
                    ),
                    const SizedBox(
                      width: 10,
                    ),
                    Column(
                      mainAxisAlignment: MainAxisAlignment.spaceAround,
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: const [
                        Text(
                          'Hi, Bao',
                          style: TextStyle(
                            fontSize: 14,
                            fontWeight: FontWeight.w400,
                          ),
                        ),
                        Text(
                          'Find your beauty product',
                          style: TextStyle(
                            fontSize: 16,
                            fontWeight: FontWeight.w700,
                          ),
                        ),
                      ],
                    ),
                    const Spacer(),
                    Container(
                      width: 50,
                      height: 50,
                      decoration: BoxDecoration(
                        borderRadius: BorderRadius.circular(25),
                        color: Colors.white,
                      ),
                      child: Center(
                        child: IconButton(
                          icon: const Icon(Icons.shopping_cart_outlined),
                          onPressed: () => Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => Cart(
                                 ),
                            ),
                          ),
                        ),
                      ),
                    ),
                  ],
                ),
                const SizedBox(
                  height: 20,
                ),
                const Search(),
                const SizedBox(
                  height: 30,
                ),
                SizedBox(
                  width: MediaQuery.of(context).size.width,
                  height: 180,
                  child: PageView.builder(
                    itemCount: 6,
                    itemBuilder: (context, index) => Container(
                      decoration: BoxDecoration(
                        borderRadius: BorderRadius.circular(15),
                        color: Colors.blue,
                      ),
                    ),
                  ),
                ),
                const SizedBox(
                  height: 18,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    ...List.generate(
                      6,
                      (index) => Container(
                        width: 6,
                        height: 6,
                        margin: const EdgeInsets.only(left: 2),
                        decoration: BoxDecoration(
                            borderRadius: BorderRadius.circular(3),
                            color: Colors.blue),
                      ),
                    ),
                  ],
                ),
                const SizedBox(
                  height: 30,
                ),
                const Text(
                  'Product categories',
                  style: TextStyle(
                    fontSize: 20,
                    fontWeight: FontWeight.w700,
                    color: AppColors.black,
                  ),
                ),
                const SizedBox(
                  height: 20,
                ),
                SizedBox(
                  // width: MediaQuery.of(context).size.width,
                  height: 50,
                  child: Row(
                    children: const [
                      Genres(
                        txt: 'All',
                      ),
                      Genres(txt: 'Masks'),
                      Genres(txt: 'Sun cream'),
                      // Genres(txt: 'Hair gel'),
                      // Genres(txt: 'Masks')
                    ],
                  ),
                ),
                const SizedBox(
                  height: 30,
                ),
                Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: const [
                    Text(
                      'Recommend',
                      style: TextStyle(
                        fontSize: 20,
                        fontWeight: FontWeight.w700,
                        color: AppColors.black,
                      ),
                    ),
                    Text('See all'),
                  ],
                ),
                GridView.builder(
                  physics: const NeverScrollableScrollPhysics(),
                  shrinkWrap: true,
                  itemCount: demoProduct.length,
                  gridDelegate: const SliverGridDelegateWithFixedCrossAxisCount(
                    crossAxisCount: 2,
                    crossAxisSpacing: 10,
                    mainAxisSpacing: 10,
                    childAspectRatio: 0.75,
                  ),
                  itemBuilder: (context, index) => ItemProduct(
                      product: demoProduct[index],
                      onTap: () => Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => Detail(demoProduct[index]),
                            ),
                          )),
                )
              ],
            ),
          ),
        ),
      ),
    );
  }
}
