/**
 *
 * @param {number} integer
 * @returns {number[]}
 */

function findEvenOrOdd(n) {
  return n % 2 == 0 ? "Even" : "Odd";
}

var h = new Proxy(function (n) {}, {
  construct: function (t, args) {
    return new target(...args);
  },
});

export const evenOrOdd = new Proxy(
  function (n) {
    return findEvenOrOdd(n);
  },
  {
    get: function (t, v) {
      // console.log(t());
      return t;
    },
    // apply: function(a, t) {
    //   console.log("asd");
    // },
    // ownKeys: function () {
    //   return ["a", "b"];
    // },
    // getOwnPropertyDescriptor: function (t, k) {
    //   return { value: this.get(t, k), enumerable: true, configurable: true };
    // },
    // get: function (t, v) {
    //   return findEvenOrOdd(v);
    // },
  }
);
