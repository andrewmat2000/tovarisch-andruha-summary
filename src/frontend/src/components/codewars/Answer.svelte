<script lang="ts">
  import { _ } from "svelte-i18n";
  import { Highlight } from "svelte-highlight";
  import { javascript } from "svelte-highlight/languages";

  export let code: string;
</script>

<h1>{$_("codewars-answer-code-label")}</h1>
<span>
  <Highlight language={javascript} {code} />
</span>

<style>
  @import url("https://unpkg.com/svelte-highlight/styles/github.css");
</style>