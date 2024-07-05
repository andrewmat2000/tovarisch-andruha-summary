/**
 *
 * @param {number} integer
 * @returns {number[]}
 */
export function divisors(integer) {
  const result = [];

  for (var i = 2; i <= integer / 2; i++) {
    if (integer % i == 0) {
      result.push(i);
    }
  }

  if (result.length == 0) {
    return `${integer} is prime`;
  }

  return result;
}
