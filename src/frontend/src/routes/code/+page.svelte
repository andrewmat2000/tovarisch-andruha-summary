<script lang="ts">
import Link from "@components/Link.svelte"
import { _ } from "svelte-i18n"

</script>

<Link label={$_("codewars")} link="/code/codewars"/>
<Link label={$_("test-tasks")} link="/code/test-tasks"/>