namespace CommonFramework.Testing.Database;

/// <summary>
/// Defines the database preparation strategy for integration tests:
/// schema creation, data seeding, and snapshot usage.
/// Affects performance and state reuse.
/// </summary>
public enum DatabaseInitMode
{
    /// <summary>
    /// Using snapshots.
    ///
    /// Steps:
    /// 1. An empty database schema is created (tables, keys, indexes, views).
    /// 2. A snapshot of the empty database is created (restore point).
    /// 3. A copy is restored from the snapshot and populated with test data.
    /// 4. A snapshot of the database with data is created.
    /// 5. Consumers receive copies from the final snapshot.
    ///
    /// Note:
    /// If a failure occurs during test data seeding, re-initialization
    /// continues from restoring the empty database snapshot.
    /// </summary>
    Snapshot,

    /// <summary>
    /// Without using snapshots.
    ///
    /// For each request:
    /// - the database schema is created;
    /// - test data is seeded.
    /// </summary>
    NoCaching,

    /// <summary>
    /// An external database is used.
    ///
    /// Generation and initialization are not performed.
    /// </summary>
    External
}