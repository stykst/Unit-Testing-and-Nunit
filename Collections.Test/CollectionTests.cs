using NUnit.Framework;
using System.Linq;
using System;
using Collections;

namespace CollectionsTests
{
    public class CollectionTests
    {
        [Test]
        [Timeout(1000)]
        public void Test_Collection_1MillionItems()
        {
            // Arrange
            const int itemsCount = 1000000;
            var nums = new Collection<int>();

            // Act
            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());

            // Assert
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);

            for (int i = itemsCount - 1; i >= 0; i--)
            {
                nums.RemoveAt(i);
            }

            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }

        [Test]
        public void Test_Collection_Add()
        {
            // Arrange
            var nums = new Collection<int>();

            // Act
            nums.Add(5);
            nums.Add(6);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[5, 6]"));
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            // Arrange
            var nums = new Collection<int>();

            // Act
            nums.AddRange(5, 6, 7, 8, 9);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[5, 6, 7, 8, 9]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            // Arrange
            var nums = new Collection<int>();
            var oldCapacity = nums.Capacity;
            var newNums = Enumerable.Range(1000, 2000).ToArray();
            var expectedNums = "[" + string.Join(", ", newNums) + "]";

            // Act
            nums.AddRange(newNums);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_AddWithGrow()
        {
            // Arrange
            var nums = new Collection<int>();
            var oldCapacity = nums.Capacity;

            // Act
            for (int i = 1; i <= 10; i++)
            {
                nums.Add(i);
            }
            string expectedNums = string.Join(", ", nums);

            // Assert
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.ToString(), Is.EqualTo(expectedNums));
        }

        [Test]
        public void Test_Collection_Clear()
        {
            // Arrange
            var nums = new Collection<int>();

            // Act
            nums.Clear();

            // Assert
            Assert.That(nums.Count == 0);
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            // Arrange
            var nums = new Collection<int>(5, 10, 15);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[5, 10, 15]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            //Arrange
            var nums = new Collection<int>(5);

            // Assert
            Assert.That(nums[0] == 5);
            Assert.That(nums.Count == 1);
            Assert.That(nums.Capacity >= nums.Count);
        }

        [Test]
        public void Test_Collection_CountAndCapacity()
        {
            // Arrange
            var nums = new Collection<int>();

            //Act
            for (int i = 1; i <= 100; i++)
            {
                nums.Add(i);
            }

            // Assert
            Assert.That(nums.Count == 100);
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            // Arrange
            var nums = new Collection<int>();

            // Assert
            Assert.That(nums.Count == 0);

        }

        [Test]
        public void Test_Collection_ExchangeFirstLast()
        {
            // Arrange
            var nums = new Collection<int>(1, 2, 3, 4);

            // Act
            nums.Exchange(0, 3);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[4, 2, 3, 1]"));
        }

        [Test]
        public void Test_Collection_ExchangeInvalidIndexes()
        {
            // Arrange
            var nums = new Collection<int>(1, 2, 3, 4);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => nums.Exchange(0, 5));
        }

        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            // Arrange
            var nums = new Collection<int>(1, 2, 3, 4);

            // Act
            nums.Exchange(1, 2);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[1, 3, 2, 4]"));
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            // Arrange
            var names = new Collection<string>("Peter", "Maria");

            // Act
            var item0 = names[0];
            var item1 = names[1];

            // Assert
            Assert.That(item0, Is.EqualTo("Peter"));
            Assert.That(item1, Is.EqualTo("Maria"));

        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            // Arrange
            var names = new Collection<string>("Bob", "Joe");

            // Assert
            Assert.That(() => { var name = names[-1]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[2]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = names[500]; },
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Bob, Joe]"));

        }

        [Test]
        public void Test_Collection_InsertAtEnd()
        {
            // Arrange
            var nums = new Collection<int>(1, 2, 3, 4);

            // Act
            nums.InsertAt(4, 5);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 3, 4, 5]"));
        }

        [Test]
        public void Test_Collection_InsertAtInvalidIndex()
        {
            // Arrange
            var names = new Collection<string>("Bob", "Joe");

            // Assert
            Assert.That(() => names.InsertAt(-1, "Pesho"),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => names.InsertAt(3, "Gosho"),
              Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(names.ToString(), Is.EqualTo("[Bob, Joe]"));
        }

        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            // Arrange
            var nums = new Collection<int>(1, 2, 4, 5);

            // Act
            nums.InsertAt(2, 3);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 3, 4, 5]"));
        }

        [Test]
        public void Test_Collection_InsertAtStart()
        {
            // Arrange
            var nums = new Collection<int>(2, 3, 4, 5);

            // Act
            nums.InsertAt(0, 1);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 3, 4, 5]"));
        }

        [Test]
        public void Test_Collection_InsertAtWithGrow()
        {
            // Arrange
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;

            // Act
            for (int i = 0; i <= 4; i++)
            {
                nums.InsertAt(i, i + 1);
            }

            // Assert
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 3, 4, 5]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_RemoveAll()
        {
            // Arrange
            var nums = new Collection<int>(1, 2, 3, 4, 5);

            // Act
            for (int i = 0; i < 5; i++)
            {
                nums.RemoveAt(0);
            }

            // Assert
            Assert.That(nums.Count == 0);
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            // Arrange
            var nums = new Collection<int>(1, 2, 3, 4, 5);

            // Act
            nums.RemoveAt(4);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 3, 4]"));
        }

        [Test]
        public void Test_Collection_RemoveAtInvalidIndex()
        {
            // Arrange
            var nums = new Collection<int>(1, 2, 3, 4, 5);

            // Assert
            Assert.That(() => nums.RemoveAt(-1), Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => nums.RemoveAt(5), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            // Arrange
            var nums = new Collection<int>(1, 2, 3, 4, 5);

            // Act
            nums.RemoveAt(2);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 4, 5]"));
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            // Arrange
            var nums = new Collection<int>(1, 2, 3, 4, 5);

            // Act
            nums.RemoveAt(0);

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[2, 3, 4, 5]"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            // Arrange
            var nums = new Collection<int>(1, 2, 3, 4, 5);

            // Act
            nums[2] = 10;

            // Assert
            Assert.That(nums.ToString(), Is.EqualTo("[1, 2, 10, 4, 5]"));
        }

        [Test]
        public void Test_Collection_SetByInvalidIndex()
        {
            // Arrange
            var nums = new Collection<int>(1, 2, 3, 4, 5);

            // Assert
            Assert.That(() => { nums[-1] = 10; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { nums[5] = 10; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_ToStringCollectionOfCollections()
        {
            // Arrange
            var nums1 = new Collection<int>(1, 2);
            var nums2 = new Collection<int>(3, 4);
            var allNums = new Collection<object>(nums1, nums2);

            // Assert
            Assert.That(allNums.ToString(), Is.EqualTo("[[1, 2], [3, 4]]"));
        }

        [Test]
        public void Test_Collection_ToStringEmpty()
        {
            // Arrange
            var nums = new Collection<string>(String.Empty);

            // Assert
            Assert.That(nums.Count == 1);
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ToStringMultiple()
        {
            // Arrange
            var objects = new Collection<object>(1, "Dimo", 20.001);

            // Assert
            Assert.That(objects.ToString(), Is.EqualTo("[1, Dimo, 20.001]"));
        }

        [Test]
        public void Test_Collection_ToStringSingle()
        {
            // Arrange
            var names = new Collection<string>("Dimo");

            // Assert
            Assert.That(names.ToString(), Is.EqualTo("[Dimo]"));
        }
    }
}