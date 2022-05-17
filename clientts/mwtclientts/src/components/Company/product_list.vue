<template>
  <div>
    <v-col class="d-flex flex-row">
      <v-text-field v-model="search" label="Search" class="mx-4"></v-text-field>
      <v-btn to="addproduct" class="mt-3" color="success">Add Product</v-btn>
    </v-col>

    <table class="fl-table">
      <thead>
        <tr>
          <th>Id</th>
          <th>Company</th>
          <th>Name</th>
          <th>Details</th>
          <th>Price</th>
          <!-- <th>Image</th> -->
          <th>Delete</th>
          <th>Update</th>
          <th>UpdateStock</th>
          <th>UpdateOffer</th>
        </tr>
      </thead>

      <tr v-for="item in filteredList" v-bind:key="item.id">
        <td>{{ item.id }}</td>
        <td>{{ item.ProdCompanyName }}</td>
        <td>{{ item.ProdName }}</td>
        <td>{{ item.ProdDetails }}</td>
        <td>{{ item.ProdPrice }}</td>
        <!-- <td>
          <img v-if="item.ProdImage" :src="imagepath" />
        </td> -->

        <td>
          <v-btn color="red" class="mt-3" @click="delProduct(item.id)"
            >Delete</v-btn
          >
        </td>
        <td>
          <UpdateProduct :productid="item.id" class="mt-3" />
        </td>
        <td><UpdateStock :productid="item.id" class="mt-3" /></td>
        <td><UpdateOffer :productid="item.id" class="mt-3" /></td>
      </tr>
    </table>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Prop, Watch } from "vue-property-decorator";
import { namespace } from "vuex-class";
import Category from "@/interfaces/category";
import Product from "@/interfaces/product";
import UpdateProduct from "@/components/Company/update_product.vue";
import UpdateStock from "@/components/Company/updatestock.vue";
import UpdateOffer from "@/components/Company/updateoffer.vue";
import { locales } from "moment";
const Products = namespace("product");
@Component({
  components: {
    UpdateProduct,
    UpdateStock,
    UpdateOffer,
  },
})
export default class ProductList extends Vue {
  category = new Category();

  public product = [] as Product[];
  pro = new Product();
  @Prop(Number)
  productid!: number | null;

  @Products.Action
  public GetAllProducts!: (data: any) => Promise<any>;
  @Products.Action
  public DeleteProduct!: (data: any) => Promise<any>;
  search = "";
  imagepath = "";

  public retrieveProduct(): void {
    this.GetAllProducts(this.product).then((data) => {
      var jdata = JSON.parse(data.content);

      if (jdata.ProdImage != null) {
        this.imagepath =
          "https://localhost:5001/StaticImages/Product/DefaultProduct.png";
      } else {
        this.imagepath =
          "https://localhost:5001/StaticImages/Product/" + this.pro.ProdImage;
      }
      this.product = jdata;
    });
  }
  public delProduct(id: any): void {
    // console.log(id);

    this.DeleteProduct(id).then((res) => {
      console.log("data", res.data);
      location.reload();
    });
  }

  mounted() {
    this.retrieveProduct();
  }

  get filteredList() {
    return this.product.filter((post: any) => {
      return (
        post.ProdCompanyName.toLowerCase().includes(
          this.search.toLowerCase()
        ) || post.ProdName.toLowerCase().includes(this.search.toLowerCase())
      );
    });
  }
  // @Watch("product.ProdImage")
  // onimagechage() {
  //   if (this.product == null) {
  //     this.imagepath =
  //       "https://localhost:5001/StaticImages/Product/DefaultProduct.png";
  //   } else {
  //     this.imagepath =
  //       "https://localhost:5001/StaticImages/Product/" + this.pro.ProdImage;
  //   }
  // }
}
</script>
<style scoped>
.update {
  background: #2a77db;
  color: #fff;
  border: none;
  padding: 5px 9px;
  border-radius: 50%;
  cursor: pointer;
  float: right;
}
* {
  box-sizing: border-box;
  -webkit-box-sizing: border-box;
  -moz-box-sizing: border-box;
}
body {
  font-family: Helvetica;
  -webkit-font-smoothing: antialiased;
  background: rgba(71, 147, 227, 1);
}
h2 {
  text-align: center;
  font-size: 18px;
  text-transform: uppercase;
  letter-spacing: 1px;
  color: white;
  padding: 30px 0;
}

/* Table Styles */

.table-wrapper {
  margin: 10px 70px 70px;
  box-shadow: 0px 35px 50px rgba(0, 0, 0, 0.2);
}

.fl-table {
  border-radius: 5px;

  font-size: 1.1em;
  border: none;
  border-collapse: collapse;
  width: 100%;
  max-width: 100%;
  white-space: nowrap;
  background-color: white;
}

.fl-table td,
.fl-table th {
  text-align: center;
  padding: 8px;
}

.fl-table td {
  border-right: 1px solid #f8f8f8;
  font-size: 1.1em;
}

.fl-table thead th {
  color: #ffffff;
  background: #2a77db;
}

.fl-table thead th:nth-child(odd) {
  color: #ffffff;
  background: #324960;
}

.fl-table tr:nth-child(even) {
  background: #f8f8f8;
}

/* Responsive */

@media (max-width: 767px) {
  .fl-table {
    display: block;
    width: 100%;
  }
  .table-wrapper:before {
    content: "Scroll horizontally >";
    display: block;
    text-align: right;
    font-size: 1.1em;
    color: white;
    padding: 0 0 10px;
  }
  .fl-table thead,
  .fl-table tbody,
  .fl-table thead th {
    display: block;
  }
  .fl-table thead th:last-child {
    border-bottom: none;
  }
  .fl-table thead {
    float: left;
  }
  .fl-table tbody {
    width: auto;
    position: relative;
    overflow-x: auto;
  }
  .fl-table td,
  .fl-table th {
    padding: 20px 0.625em 0.625em 0.625em;
    height: 60px;
    vertical-align: middle;
    box-sizing: border-box;
    overflow-x: hidden;
    overflow-y: auto;
    width: 120px;
    font-size: 13px;
    text-overflow: ellipsis;
  }
  .fl-table thead th {
    text-align: left;
    border-bottom: 1px solid #f7f7f9;
  }
  .fl-table tbody tr {
    display: table-cell;
  }
  .fl-table tbody tr:nth-child(odd) {
    background: none;
  }
  .fl-table tr:nth-child(even) {
    background: transparent;
  }
  .fl-table tr td:nth-child(odd) {
    background: #f8f8f8;
    border-right: 1px solid #e6e4e4;
  }
  .fl-table tr td:nth-child(even) {
    border-right: 1px solid #e6e4e4;
  }
  .fl-table tbody td {
    display: block;
    text-align: center;
  }
}
</style>
