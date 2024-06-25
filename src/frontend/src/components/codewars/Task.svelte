<script lang="ts">
  import { Highlight } from "svelte-highlight";
  import { bash } from "svelte-highlight/languages";

  import { _ } from "svelte-i18n";

  export let title: string;
  export let text: string;
  export let example: string;
</script>

<div>
  <h1>{title}</h1>
  <span>{text}</span>
  <br />
  <h2>{$_("codewars-example-label")}</h2>

  <Highlight language={bash} code={example} />
</div>
