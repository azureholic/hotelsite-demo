import { BottomBar, Slide } from '@deckio/deck-engine'
import styles from './ToolingSlide.module.css'

export default function ToolingSlide({ index, project }) {
  return (
    <Slide index={index} className={styles.toolingSlide}>
      <div className="accent-bar" />
      <div className={`orb ${styles.orb1}`} />
      <div className={`orb ${styles.orb2}`} />

      <div className={`${styles.body} content-frame content-gutter`}>
        <div className={styles.header}>
          <p className={styles.eyebrow}>02 — Tooling</p>
          <h1>GitHub Copilot — What Exists Today</h1>
          <p className={styles.subtitle}>
            A full AI layer across your editor, terminal, and CI pipeline.
          </p>
        </div>

        <div className={styles.cards}>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Agents</h3>
            <p className={styles.cardText}>
              Autonomous AI that plans, edits files across your project, runs
              commands, and self-corrects — locally, in background, or in the
              cloud.
            </p>
          </div>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Inline Suggestions</h3>
            <p className={styles.cardText}>
              Real-time code completions as you type — from single lines to full
              function implementations, plus next-edit predictions.
            </p>
          </div>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Chat &amp; Inline Chat</h3>
            <p className={styles.cardText}>
              Ask questions, request refactors, or get explanations. Inline chat
              (Ctrl+I) applies changes directly in the editor.
            </p>
          </div>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Copilot CLI</h3>
            <p className={styles.cardText}>
              Terminal intelligence: explain commands, draft shell scripts,
              troubleshoot errors, and discover new tools — all from the command
              line.
            </p>
          </div>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Smart Actions</h3>
            <p className={styles.cardText}>
              One-click AI actions: fix errors, generate commit messages, rename
              symbols, and run semantic search across your project.
            </p>
          </div>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Customization</h3>
            <p className={styles.cardText}>
              Custom instructions, agent skills, MCP servers, and hooks — tailor
              the AI to your team&apos;s conventions and workflows.
            </p>
          </div>
        </div>
      </div>

      <BottomBar text="Vibe Coding with GitHub Copilot CLI" />
    </Slide>
  )
}
