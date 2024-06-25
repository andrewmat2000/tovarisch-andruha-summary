/**
 *
 * @param {string} text
 * @returns {number}
 */
export function duplicateCount(text) {
  let map = {};
  let counter = 0;

  for (let char of text.toLowerCase()) {
    if (map[char] == undefined) {
      map[char] = 1;
      continue;
    }
    if (map[char] > 1) {
      continue;
    }
    map[char] = 2;
    counter++;
  }

  return counter;
}
