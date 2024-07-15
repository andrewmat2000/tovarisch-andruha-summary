/**
 *
 * @param {string} text
 * @returns {boolean}
 */
export function validatePIN(text) {
  return /^((\d{4})|(\d{6}))$/.test(text);
}
